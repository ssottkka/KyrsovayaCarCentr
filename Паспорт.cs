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
    
    public partial class Паспорт
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Паспорт()
        {
            this.Сотрудники = new HashSet<Сотрудники>();
        }
    
        public int Код { get; set; }
        public string Серия { get; set; }
        public string Номер { get; set; }
        public System.DateTime Дата_выдачи { get; set; }
        public string Кем_выдан { get; set; }
        public System.DateTime Дата_рождения { get; set; }
        public string Место_рождения { get; set; }
        public string Место_прописки { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Сотрудники> Сотрудники { get; set; }
    }
}
