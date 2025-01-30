using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;
using MyApp;

namespace MyApp.ServiceModel;

[Tag("Bookings")]
[Route("/bookings/{Id}","GET")]
[Route("/bookings","GET")]
[AutoApply(Behavior.AuditQuery)]
[Description("Find Bookings")]
[ValidateHasRole("Employee")]
public class QueryBookings : QueryDb<Booking>
{
    public int? Id { get; set; }
    public List<int>? Ids { get; set; }
}

[Tag("Bookings")]
[Route("/bookings","POST")]
[AutoApply(Behavior.AuditCreate)]
[Description("Create a new Booking")]
[ValidateHasRole("Employee")]
public class CreateBooking : ICreateDb<Booking>, IReturn<IdResponse>
{
    [ValidateNotEmpty]
    public string Name { get; set; }
    public RoomType RoomType { get; set; }
    [ValidateGreaterThan(0)]
    public int RoomNumber { get; set; }
    public DateTime BookingStartDate { get; set; }
    public DateTime? BookingEndDate { get; set; }
    [ValidateGreaterThan(0)]
    public decimal Cost { get; set; }
    public string? CouponId { get; set; }
    [Input(Type="textarea")]
    public string? Notes { get; set; }
    public bool? Cancelled { get; set; }
}

[Tag("Bookings")]
[Notes("Find out how to quickly create a <a class='svg-external' target='_blank' href='https://youtu.be/nhc4MZufkcM'>C# Bookings App from Scratch</a>")]
[Route("/booking/{Id}","PATCH")]
[AutoApply(Behavior.AuditModify)]
[Description("Update an existing Booking")]
[ValidateHasRole("Employee")]
public class UpdateBooking : IPatchDb<Booking>, IReturn<IdResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public RoomType? RoomType { get; set; }
    [ValidateGreaterThan(0)]
    public int? RoomNumber { get; set; }
    public DateTime? BookingStartDate { get; set; }
    public DateTime? BookingEndDate { get; set; }
    [ValidateGreaterThan(0)]
    public decimal? Cost { get; set; }
    public string? CouponId { get; set; }
    [Input(Type="textarea")]
    public string? Notes { get; set; }
    public bool? Cancelled { get; set; }
}

[Tag("Bookings")]
[Route("/booking/{Id}","DELETE")]
[AutoApply(Behavior.AuditSoftDelete)]
[Description("Delete a Booking")]
[ValidateHasRole("Employee")]
public class DeleteBooking : IDeleteDb<Booking>, IReturnVoid
{
    public int? Id { get; set; }
    public List<int>? Ids { get; set; }
}

[Tag("Bookings")]
[AutoApply(Behavior.AuditQuery)]
public class QueryCoupons : QueryDb<Coupon>
{
    public string? Id { get; set; }
    public List<string>? Ids { get; set; }
}

[Tag("Bookings")]
[AutoApply(Behavior.AuditCreate)]
[ValidateIsAuthenticated]
public class CreateCoupon : ICreateDb<Coupon>, IReturn<IdResponse>
{
    [ValidateNotEmpty]
    public string Id { get; set; }
    [ValidateNotEmpty]
    public string Description { get; set; }
    public decimal Discount { get; set; }
    public DateTime ExpiryDate { get; set; }
}

[Tag("Bookings")]
[AutoApply(Behavior.AuditModify)]
[ValidateIsAuthenticated]
public class UpdateCoupon : IPatchDb<Coupon>, IReturn<IdResponse>
{
    public string Id { get; set; }
    public string? Description { get; set; }
    public decimal? Discount { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

[Tag("Bookings")]
[AutoApply(Behavior.AuditSoftDelete)]
[ValidateIsAuthenticated]
public class DeleteCoupon : IDeleteDb<Coupon>, IReturnVoid
{
    public string? Id { get; set; }
    public List<string>? Ids { get; set; }
}


[Icon(Svg="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='currentColor' d='M16 10H8c-.55 0-1 .45-1 1s.45 1 1 1h8c.55 0 1-.45 1-1s-.45-1-1-1zm3-7h-1V2c0-.55-.45-1-1-1s-1 .45-1 1v1H8V2c0-.55-.45-1-1-1s-1 .45-1 1v1H5a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm-1 16H6c-.55 0-1-.45-1-1V8h14v10c0 .55-.45 1-1 1zm-5-5H8c-.55 0-1 .45-1 1s.45 1 1 1h5c.55 0 1-.45 1-1s-.45-1-1-1z'/></svg>")]
[Notes("Captures a Persons Name & Room Booking information")]
[Description("Booking Details")]
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
    [Format(FormatMethods.Hidden)]
    [Reference]
    public Coupon? Discount { get; set; }
    public string? Notes { get; set; }
    public bool? Cancelled { get; set; }
    [Format(FormatMethods.Hidden)]
    [Reference(SelfId=nameof(CreatedBy),RefId=nameof(User.UserName),RefLabel=nameof(User.DisplayName))]
    public User Employee { get; set; }
}

[Icon(Svg="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='currentColor' d='M2 9.5V4a1 1 0 0 1 1-1h18a1 1 0 0 1 1 1v5.5a2.5 2.5 0 1 0 0 5V20a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1v-5.5a2.5 2.5 0 1 0 0-5zm2-1.532a4.5 4.5 0 0 1 0 8.064V19h16v-2.968a4.5 4.5 0 0 1 0-8.064V5H4v2.968zM9 9h6v2H9V9zm0 4h6v2H9v-2z' /></svg>")]
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

