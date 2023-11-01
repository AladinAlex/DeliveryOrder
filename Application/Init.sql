insert into [order].City(Name)
values ('Krasnodar'),
('Kazan'),
('Moscow'),
('Rostov-on-Don'),
('Stavropol'),
('Saint Petersburg'),
('Voronezh'),
('Kaluga'),
('Волгоград'),
('Bryansk');

insert into [order].Truck(Name, Weight)
values
('truck1', 30.0),
('truck2', 50.0),
('truck3', 35.0),
('truck4', 15.00),
('truck5', 10.00),
('truck6', 25.00),
('truck7', 10.0),
('truck8', 40.0),
('truck9', 45.0);

insert into [order].PriceList(CityStartId, CityFinishId, Price, DurationDay)
select c1.Id, c2.Id,
(SELECT ABS(CHECKSUM(NEWID())) % 100000 + 1 AS RandomNumber),
(SELECT ABS(CHECKSUM(NEWID())) % 10 + 1 AS RandomNumber)
from [order].City c1
cross join [order].City c2
where c1.Id <> c2.Id

go;
CREATE or alter PROCEDURE [order].[GetFreeTruckId]
        @weight float
AS
BEGIN

-- максимальная длительность, чтобы отсечь "очень старые заказы"
declare @maxDuration int = (select max(DurationDay) from [order].PriceList);
	select *
    into #t1
    from [order].[Order] o
    where DATEADD(day,-@maxDuration, o.PickupDt) < getdate()

-- для каждого подходящего заказа ищем его прайс лист
    select o.*, prList.Id as priceListId, prList.DurationDay
    into #t2
    from #t1 o
    inner join Address adrSend on adrSend.Id = o.AddressSenderId
    inner join Address adrRec on adrRec.Id = o.AddressRecipientId
    inner join City cS on cS.Id = adrSend.сityId
    inner join City cR on cR.Id = adrRec.сityId
    outer apply(select top 1 Id, DurationDay from PriceList pl where pl.CityStartId = cS.Id and pl.CityFinishId = cR.Id) as prList

-- удаляем те, что свободны, остались занятые
    delete t from #t2 t where DATEADD(day, t.DurationDay, t.PickupDt) < getdate()

    select top 1 * from [order].Truck where Id not in (select TruckId from #t2) and weight >= @weight

drop table #t1;
drop table #t2;
END
GO
