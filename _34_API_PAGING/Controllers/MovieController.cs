﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _34_API_PAGING.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;
        private readonly IUrlHelper _urlHelper;

        public MoviesController(IMovieService service, IUrlHelper urlHelper)
        {
            _service = service;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult Get(PagingParams pagingParams)
        {
            var model = _service.GetMovies(pagingParams);
            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());
            var outputModel = new MovieOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model),
                Items = model.List.Select(m => ToMovieInfo(m)).ToList(),
            };
            return Ok(outputModel);
        }

        #region " Links "
        private List<LinkInfo> GetLinks(PagedList<Movie> list)
        {
            var links = new List<LinkInfo>();
            if (list.HasPreviousPage)
                links.Add(CreateLink("GetMovies", list.PreviousPageNumber, list.PageSize, "previousPage", "GET"));
            links.Add(CreateLink("GetMovies", list.PageNumber, list.PageSize, "self", "GET"));
            if (list.HasNextPage)
                links.Add(CreateLink("GetMovies", list.NextPageNumber, list.PageSize, "nextPage", "GET"));
            return links;
        }

        private LinkInfo CreateLink(string routeName, int pageNumber, int pageSize, string rel, string method)
        {
            return new LinkInfo
            {
                Href = _urlHelper.Link(routeName, new { PageNumber = pageNumber, PageSize = pageSize }),
                Rel = rel,
                Method = method
            };
        }
        #endregion

        #region " Mappings "
        private MovieInfo ToMovieInfo(Movie model)
        {
            return new MovieInfo
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary,
                LastReadAt = DateTime.Now
            };
        }
        #endregion
    }
}
