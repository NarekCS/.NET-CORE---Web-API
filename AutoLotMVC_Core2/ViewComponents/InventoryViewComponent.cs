using AutoLotDAL_Core2.Models;
using AutoLotDAL_Core2.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoLotMVC_Core2.ViewComponents
{
    public class InventoryViewComponent : ViewComponent
    {
        private readonly string _baseUrl;
        public InventoryViewComponent(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("ServiceAddress").Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var items = JsonConvert.DeserializeObject<List<Inventory>>(await response.Content.ReadAsStringAsync());
                return View("InventoryPartialView", items);
            }
            return new ContentViewComponentResult("Unable to return records.");
        }

        //private readonly IInventoryRepo _repo;
        //public InventoryViewComponent(IInventoryRepo repo)
        //{
        //    _repo = repo;
        //}
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var cars = _repo.GetAll(x => x.Make, true);
        //    if (cars != null)
        //    {
        //        return View("InventoryPartialView", cars);
        //    }
        //    return new ContentViewComponentResult("Unable to locate records.");
    }
}

