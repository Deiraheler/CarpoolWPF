//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Carpool_App
{
    using System;
    using System.Collections.Generic;
    
    public partial class History
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public int PostsId { get; set; }
        public string As { get; set; }
    
        public virtual Users User { get; set; }
        public virtual Posts Post { get; set; }
    }
}