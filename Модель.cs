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
    
    public partial class Модель
    {
        public int Код_модели { get; set; }
        public int Код_марки { get; set; }
        public string Название { get; set; }
    
        public virtual Марка Марка { get; set; }
    }
}
