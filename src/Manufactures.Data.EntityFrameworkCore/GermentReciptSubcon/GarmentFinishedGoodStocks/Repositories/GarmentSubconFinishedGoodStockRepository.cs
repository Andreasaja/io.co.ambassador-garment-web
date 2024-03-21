﻿using Infrastructure.Data.EntityFrameworkCore;
using Infrastructure.Data.EntityFrameworkCore.Utilities;
using Manufactures.Domain.GermentReciptSubcon.GarmentFinishedGoodStocks;
using Manufactures.Domain.GermentReciptSubcon.GarmentFinishedGoodStocks.ReadModels;
using Manufactures.Domain.GermentReciptSubcon.GarmentFinishedGoodStocks.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manufactures.Data.EntityFrameworkCore.GermentReciptSubcon.GarmentFinishedGoodStocks.Repositories
{
    public class GarmentSubconFinishedGoodStockRepository : AggregateRepostory<GarmentSubconFinishedGoodStock, GarmentSubconFinishedGoodStockReadModel>, IGarmentSubconFinishedGoodStockRepository
    {
        public IQueryable<GarmentSubconFinishedGoodStockReadModel> Read(int page, int size, string order, string keyword, string filter)
        {
            var data = Query;

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            data = QueryHelper<GarmentSubconFinishedGoodStockReadModel>.Filter(data, FilterDictionary);

            List<string> SearchAttributes = new List<string>
            {
                "FinishedGoodStockNo",
                "Article",
                "RONo",
                "UnitCode",
                "UnitName",
            };
            data = QueryHelper<GarmentSubconFinishedGoodStockReadModel>.Search(data, SearchAttributes, keyword);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            data = OrderDictionary.Count == 0 ? data.OrderByDescending(o => o.ModifiedDate) : QueryHelper<GarmentSubconFinishedGoodStockReadModel>.Order(data, OrderDictionary);

            //data = data.Skip((page - 1) * size).Take(size);

            return data;
        }

        public IQueryable<GarmentSubconFinishedGoodStockReadModel> ReadComplete(int page, int size, string order, string keyword, string filter)
        {
            var data = Query;

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            data = QueryHelper<GarmentSubconFinishedGoodStockReadModel>.Filter(data, FilterDictionary);

            List<string> SearchAttributes = new List<string>
            {
                "RONo",
            };
            data = QueryHelper<GarmentSubconFinishedGoodStockReadModel>.Search(data, SearchAttributes, keyword);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            data = OrderDictionary.Count == 0 ? data.OrderByDescending(o => o.ModifiedDate) : QueryHelper<GarmentSubconFinishedGoodStockReadModel>.Order(data, OrderDictionary);

            //data = data.Skip((page - 1) * size).Take(size);

            return data;
        }


        protected override GarmentSubconFinishedGoodStock Map(GarmentSubconFinishedGoodStockReadModel readModel)
        {
            return new GarmentSubconFinishedGoodStock(readModel);
        }
    }
}