using System.Data;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MyApp;

namespace MyApp.Migrations;

public class Migration1001 : MigrationBase
{

    [Notes("Captures a Persons Name & Room Booking information")]
    public class Booking : AuditBase
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public RoomType RoomType { get; set; }
        public int RoomNumber { get; set; }
        [IntlDateTime(DateStyle.Long)]
        public DateTime BookingStartDate { get; set; }
        [IntlRelativeTime]
        public DateTime? BookingEndDate { get; set; }
        [IntlNumber(Currency="USD")]
        public decimal Cost { get; set; }
        [Ref(Model=nameof(Coupon),RefId=nameof(Coupon.Id),RefLabel=nameof(Coupon.Description))]
        [References(typeof(Coupon))]
        public string? CouponId { get; set; }
        public string? Notes { get; set; }
        public bool? Cancelled { get; set; }
    }
    
    public class Coupon : AuditBase
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
    
    public enum RoomType
    {
        Single,
        Double,
        Queen,
        Twin,
        Suite,
    }
    

    public override void Up()
    {
        Db.CreateTable<Coupon>();
        Db.CreateTable<Booking>();
    }

    public override void Down()
    {
        Db.DropTable<Booking>();
        Db.DropTable<Coupon>();
    }
}