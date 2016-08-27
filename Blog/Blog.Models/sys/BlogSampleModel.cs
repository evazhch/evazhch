using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Models.sys
{
   public class BlogSampleModel
    {
       [Display(Name="ID")]
       public string Id { get; set; }    //id
       [Display(Name="名称")]
       public string Name { get; set; }   //名称
       [Display(Name="简介")]
       public string Notice { get; set; }   //简介
       [Display(Name="作者")]
       public string Author { get; set; }  //作者
       [Display(Name="浏览次数")]
       public int? Brows { get; set; } //浏览次数
      [Display(Name="推荐次数")]
       public int? Recommend { get; set; }
       [Display(Name="地址")]
       public string Addr { get; set; }
       [Display(Name="创建时间")]
       public DateTime? CreateTime { get; set; } //创建时间

    }
}
