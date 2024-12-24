//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CARCENTRdb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Автомобили
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Автомобили()
        {
            this.Сделка = new HashSet<Сделка>();
            this.Услуги = new HashSet<Услуги>();
        }
    
        public int Код_авто { get; set; }
        public int Код_цвета { get; set; }
        public int Код_комплектации { get; set; }
        public int Год_выпуска { get; set; }
        public decimal Цена { get; set; }
        public int Количество_владельцев { get; set; }
        public int Код_состояния { get; set; }
        public string СТС { get; set; }
        public string ПТС { get; set; }
        public int Код_марки { get; set; }
    
        public virtual Комплектация Комплектация { get; set; }
        public virtual Марка Марка { get; set; }
        public virtual Состояние Состояние { get; set; }
        public virtual Цвет Цвет { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Сделка> Сделка { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Услуги> Услуги { get; set; }
    }
}
