using AutoMapper;

namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record Delivery(
        Decimal TTN,
        Int32 Status,
        String StatusDescription,
        Boolean Printed,
        String Address,
        String Sender,
        DeliveryServiceType DeliveryServiceType,
        String CreatedAt,
        String ArrivedAt,
        String RecipentAt,
        String PayedKeepingAt,
        String ScheduledDeliveryAt,
        Decimal TotalAmount,
        String Total,
        Decimal ShippingRateAmount,
        String ShippingRate,
        Boolean NovaPay,
        Boolean International,
        ProfileShort Profile,
        ClientShort Client,
        AgreementShort Agreement
        );

    public enum DeliveryServiceType
    {
        NovaPoshta,
        UrkPoshta,
        Justin,
        Manual
    }
}
