using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CategoriesController : BaseController<Category, CategoryRepository, int>
    {
        private readonly CategoryRepository categoryRepository;
        public CategoriesController(CategoryRepository categoryRepository) : base(categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
    }
}