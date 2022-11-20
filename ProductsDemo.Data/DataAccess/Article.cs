using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsDemo.Data.DataAccess;

[Table("ARTICLE")]
public partial class Article
{
    [Key]
    [Column("ARTICLEID")]
    public Guid Articleid { get; set; }

    [Column("ARTICLENAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Articlename { get; set; }

    [Column("COLOURID")]
    public Guid? Colourid { get; set; }

    [Column("COLORNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Colorname { get; set; }

    [Column("COLORCODE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Colorcode { get; set; }

    [Column("PRODUCTID")]
    public Guid? Productid { get; set; }

    [ForeignKey("Productid")]
    [InverseProperty("Articles")]
    public virtual Product? Product { get; set; }
}
