﻿using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Movi.Common;
using Movi.IGrabService;
using Movi.ImaxGrabService.GrabHelper;
using Movi.Model;
using Movi.Model.Enums;

namespace Movi.ImaxGrabService
{
    public class CartoonGrabService : GrabBase<Media>, ICartoonGrabService
    {
        protected override Media GetEntityDetailInfos(string detailContent)
        {
            if (String.IsNullOrEmpty(detailContent)) return null;

            var document = new HtmlDocument();
            document.LoadHtml(detailContent);

            var rootNode = document.DocumentNode;
            var entityInfo = rootNode.SelectSingleNode(Configer.EntityDetailXPath);

            //动漫封面
            var entityCover = "";
            var entityCoverNode = entityInfo.SelectSingleNode("//div[1]/div[3]/div[1]/img");
            if (entityCoverNode != null)
            {
                entityCover = entityCoverNode.Attributes["src"].Value;
            }

            //动漫名称
            var entityName = "";
            var entityNameNode = entityInfo.SelectSingleNode("//div[1]/div[3]/h1/span[1]");
            if (entityNameNode != null)
            {
                entityName = entityNameNode.InnerText;
            }

            //动漫别名
            var entityAnotherName = "";
            var entityAnotherNameNode = entityInfo.SelectSingleNode("//div[1]/div[3]/ul/li[1]/h2");
            if (entityAnotherNameNode != null)
            {
                entityAnotherName = entityAnotherNameNode.InnerText;
            }

            //动漫年份
            var entityYear = "";
            var entityYearNode = entityInfo.SelectSingleNode("//div[1]/div[3]/h1/span[2]");
            if (entityYearNode != null)
            {
                entityYear = entityYearNode.InnerText;
            }

            //动漫时长
            var entityMins = "";
            var entityMinsNode = entityInfo.SelectSingleNode("//div[1]/div[3]/div[2]/span[@class='time_length']");
            if (entityMinsNode != null)
            {
                entityMins = entityMinsNode.InnerText;
            }
            //动漫地区
            var entityArea = "";
            var entityAreaNode = entityInfo.SelectSingleNode("//div[1]/div[3]/div[2]/span[@class='countries']/a");
            if (entityAreaNode != null)
            {
                entityArea = entityAreaNode.InnerText;
            }
            //动漫分类
            var categories = entityInfo.SelectNodes("//div[1]/div[3]/div[2]/span[3]/a");
            var entityCategories = new List<string>();
            if (categories != null && categories.Any())
            {
                entityCategories.AddRange(categories.Select(category => category.InnerText));
            }
            //动漫评分
            var entityRank = 0.0f;
            var entityRankNode = entityInfo.SelectSingleNode("//div[1]/div[3]/div[3]/span/span");
            if (entityRankNode != null)
            {
                entityRank = float.Parse(entityRankNode.InnerText);
            }

            //观影数
            var entityViewCount = "";
            var entityViewCountNode = entityInfo.SelectSingleNode("//div[1]/div[3]/div[3]/div/a/span");
            if (entityViewCountNode != null)
            {
                entityViewCount = entityViewCountNode.InnerText;
            }

            //外部链接
            var entityOuterLink = "";
            var entityOuterLinkNode = entityInfo.SelectSingleNode("//div[1]/div[3]/div[3]/div/a");
            if (entityOuterLinkNode != null)
            {
                entityOuterLink = entityOuterLinkNode.Attributes["href"].Value;
            }

            //动漫语言
            var entityLanguage = "";
            var entityLanguageNode = entityInfo.SelectSingleNode("//div[1]/div[3]/ul/li[2]/span/a");
            if (entityLanguageNode != null)
            {
                entityLanguage = entityLanguageNode.InnerText;
            }

            //动漫主演
            var actors = entityInfo.SelectNodes("//div[1]/div[3]/ul/li[3]/span/a");
            var entityActors = new List<string>();
            if (actors != null && actors.Any())
            {
                entityActors.AddRange(actors.Select(actor => actor.InnerText));
            }
            //动漫导演
            var entityDirector = "";
            var entityDirectorNode = entityInfo.SelectSingleNode("//div[1]/div[3]/ul/li[4]/span/a");
            if (entityDirectorNode != null)
            {
                entityDirector = entityDirectorNode.InnerText;
            }

            //动漫简介
            var entitySummary = "";
            var entitySummaryNode = entityInfo.SelectSingleNode("//div[2]/div/span");
            if (entitySummaryNode != null)
            {
                entitySummary = entitySummaryNode.InnerText;
            }
            //动漫资源
            var sources = entityInfo.SelectNodes("//div[@id='downloads']//div[1]/div[2]/table/tbody/tr");
            var entitySources = new List<MediaResource>();
            if (sources != null && sources.Any())
            {
                entitySources.AddRange(from source in sources
                    let sourceQuality = source.SelectSingleNode("td[1]").InnerText
                    let sourceSize = source.SelectSingleNode("td[2]/span").InnerText
                    let sourceAddress = source.SelectSingleNode("td[3]/a").Attributes["href"].Value
                    let sourceName = source.SelectSingleNode("td[3]/a").InnerText
                    select new MediaResource()
                    {
                        Id = StringHelper.GetGuidString(), 
                        Name = sourceName, 
                        Quality = sourceQuality, 
                        Address = sourceAddress, 
                        Size = sourceSize
                    });
            }

            var entity = new Media
            {
                Id = StringHelper.GetGuidString(), 
                Type = MediaType.Cartoon,
                Name = entityName,
                AnotherName = entityAnotherName,
                Cover = entityCover,
                AddTime = DateTime.Now,
                Director = entityDirector,
                Actors = entityActors,
                Rank = entityRank,
                ViewCount = int.Parse(entityViewCount),
                OuterLink = entityOuterLink,
                Year = ValidateHelper.GetNumber(entityYear),
                Summary = ValidateHelper.FilterWordwrapAndBlank(entitySummary),
                Area = entityArea,
                Mins = ValidateHelper.GetNumber(entityMins),
                GrabSource = Configer.GrabListUrl,
                Resources = entitySources,
                Captions = null,
                Comments = null,
                Language = entityLanguage,
                Categories = entityCategories
            };
            return entity;
        }

        protected override GrabConfiger GetConfiger()
        {
            var grabConfiger = new GrabConfiger()
            {
                GrabListUrl = "http://imax.im/movies?page={0}&tag=动画",
                GrabDetailUrl = "http://imax.im/movies/{0}",
                GrabCount = 100,
                StopAttemptTimes = 3,
                TotalPageXPath = @"//div[2]/div/ul/li[last()-1]/a",
                EntityListXPath = @"//*[@id='movies']/div[2]/section",
                EntityIdXPath = @"div[@class='cover']/a",
                EntityDetailXPath = @"//*[@id='movie']"
            };

            return grabConfiger;
        }

    }
}
