//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRental.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderAmountCustomer
    {
        public int Car_id { get; set; }
        public Nullable<System.DateTime> DateOfIssue { get; set; }
        public Nullable<System.DateTime> DateReturn { get; set; }
        public Nullable<double> AmountOrder { get; set; }
    }
}
