using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Movi.Model.Enums;

namespace Movi.Model
{
    public class Media
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 媒体类型
        /// </summary>
        public MediaType Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage="骚年，名称不能为空")]
        [Display(Name = "名称")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [Display(Name = "别名")]
        [DataType(DataType.Text)]
        public string AnotherName { get; set; }

        /// <summary>
        /// 所属地区
        /// </summary>
        [Display(Name = "地区")]
        [Required(ErrorMessage = "骚年，地区不能为空")]
        [DataType(DataType.Text)]
        public string Area { get; set; }

        /// <summary>
        /// 图片（远程）
        /// </summary>
        [Display(Name = "封面图片网络地址")]
        [Required(ErrorMessage = "骚年，封面图片地址不能为空")]
        [DataType(DataType.ImageUrl)]
        public string Cover { get; set; }

        /// <summary>
        /// 图片（本地）
        /// </summary>
        [Display(Name = "封面图片本地地址")]
        [DataType(DataType.ImageUrl)]
        public string LocalCover { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        [Required(ErrorMessage = "骚年，时长不能为空")]
        [Display(Name = "时长")]
        public int Mins { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        [Display(Name = "语言")]
        [DataType(DataType.Text)]
        public string Language { get; set; }

        /// <summary>
        /// 导演 
        /// </summary>
        [Display(Name = "导演")]
        [Required(ErrorMessage = "骚年，导演名称不能为空")]
        [DataType(DataType.Text)]
        public string Director { get; set; }

        /// <summary>
        /// 所属年份
        /// </summary>
        [Display(Name = "年份")]
        [Required(ErrorMessage = "骚年，所属所份不能为空")]
        public int Year { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Display(Name = "添加时间")]
        [DataType(DataType.DateTime)]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Display(Name = "简介")]
        [Required(ErrorMessage = "骚年，简介不能为空")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        [Range(typeof(float), "0.00", "9.99",ErrorMessage="骚年，请填写0.00 - 9.99之间的数字")]
        [Display(Name = "评分")]        
        public float Rank { get; set; }

        /// <summary>
        /// 观影数
        /// </summary>
        [Display(Name = "观影数")]                
        public int ViewCount { get; set; }

        /// <summary>
        /// 外部链接[豆瓣]
        /// </summary>
        [Display(Name = "外部链接")]
        [DataType(DataType.Url)]
        public string OuterLink { get; set; }

        /// <summary>
        /// 采集来源
        /// </summary>
        [Display(Name = "采集地址")]
        [DataType(DataType.Text)]
        public string GrabSource { get; set; }

        /// <summary>
        /// 主演列表
        /// </summary>
        public List<string> Actors
        {
            get
            {
                return !String.IsNullOrEmpty(ActorsSerialized) 
                    ? ActorsSerialized.Split(new[] { " ", "/" }, StringSplitOptions.RemoveEmptyEntries).ToList() 
                    : null;
            }
            set
            {
                if (value != null && value.Any())
                {
                    ActorsSerialized = String.Join(" / ", value);
                }
            }
        }

        /// <summary>
        /// 主演文字显示
        /// </summary>
        [Display(Name = "演员列表")]
        [Required(ErrorMessage = "骚年，演员列表不能为空")]
        [DataType(DataType.Text)]
        public string ActorsSerialized { get; set; }

        /// <summary>
        /// 主演列表
        /// </summary>
        public List<string> Categories
        {
            get
            {
                return !String.IsNullOrEmpty(CategoriesSerialized)
                    ? CategoriesSerialized.Split(new[] { " ", "/" }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    : null;
            }
            set
            {
                if (value != null && value.Any())
                {
                    CategoriesSerialized = String.Join(" / ", value);
                }
            }
        }

        /// <summary>
        /// 主演文字显示
        /// </summary>
        [Display(Name = "分类列表")]
        [Required(ErrorMessage = "骚年，分类列表不能为空")]
        [DataType(DataType.Text)]
        public string CategoriesSerialized { get; set; }

        /// <summary>
        /// 字幕列表
        /// </summary>
        public List<MediaCaption> Captions { get; set; }

        /// <summary>
        /// 资源列表
        /// </summary>
        public List<MediaResource> Resources { get; set; }

        /// <summary>
        /// 评论列表
        /// </summary>
        public List<MediaComment> Comments { get; set; }
    }
}
