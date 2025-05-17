using System.ComponentModel.DataAnnotations;

namespace OnlineOrder.Db.Models
{
    public enum Status
    {
        [Display(Name = "Создан")]
        New,

        [Display(Name = "Ожидает оплаты")]
        AwaitPayment,

        [Display(Name = "Оплачен")]
        Paid,

        [Display(Name = "Передан в доставку")]
        Delivering,

        [Display(Name = "Доставлен")]
        Delivared,

        [Display(Name = "Выполнен")]
        Done
    }
}
