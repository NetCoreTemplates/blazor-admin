export default {
    group: "Bookings",
    items: {
        Bookings: {
            type: 'Booking',
            component: {
                template:`<AutoQueryGrid :type="type"
                    selected-columns="id,name,roomType,roomNumber,bookingStartDate,bookingEndDate,cost,couponId,discount,notes,cancelled,employee" />`,
            },
        },
        Coupons: {
            type: 'Coupon',
            component: {
                template:`<AutoQueryGrid :type="type"
                    selected-columns="id,description,discount,expiryDate" />`,
            },
        },
    }
}