using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsDemo.Data.DataAccess;

[Table("PRODUCT")]
public partial class Product
{
    [Key]
    [Column("PRODUCTID")]
    public Guid Productid { get; set; }

    [Column("PRODUCTNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Productname { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("PRODUCTYEAR")]
    public int? Productyear { get; set; }

    [Column("CHANNELID")]
    public int? Channelid { get; set; }

    [Column("SIZESCALEID")]
    public Guid? Sizescaleid { get; set; }

    [Column("CREATEDBY")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Createdby { get; set; }

    [Column("CREATEDDATE", TypeName = "datetime")]
    public DateTime? Createddate { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Article> Articles { get; set; } //= new List<Article>();
}
