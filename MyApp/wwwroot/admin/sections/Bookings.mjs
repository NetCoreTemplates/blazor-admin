export default {
    group: "Bookings",
    items: {
        Bookings: {
            type: 'Booking',
            component: {
                template:`
                <AutoQueryGrid :type="type"
                    selected-columns="id,name,roomType,roomNumber,bookingStartDate,bookingEndDate,cost,couponId,discount,notes,cancelled,employee">
                  <template #formfooter="{ form, type, apis, model, id }">
                    <AuditEvents v-if="form === 'edit'" class="mt-4" :key="id" :type="type" :id="id" />
                  </template>
                </AutoQueryGrid>
                `,
            },
        },
        Coupons: {
            type: 'Coupon',
            component: {
                template:`
                <AutoQueryGrid :type="type"
                    selected-columns="id,description,discount,expiryDate">
                  <template #formfooter="{ form, type, apis, model, id }">
                    <AuditEvents v-if="form === 'edit'" class="mt-4" :key="id" :type="type" :id="id" />
                  </template>
                </AutoQueryGrid>
                `,
            },
        },
    }
}