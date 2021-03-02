using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
	[ApiController]
	[Route("api/controller")]
	public class ProfilesController : ControllerBase
	{
		private readonly ProfilesService _service;
		private readonly VaultsService _vservice;

		public ProfilesController(ProfilesService service VaultsService vservice)
		{
			_service = service;
			_vservice = vservice;
		}

		[HttpGet("{id}")]
		public ActionResult<Profile> Get(string id)
		{
			try
			{
				Profile profile = _service.GetProfileById(id);
				return Ok(profile);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}/vaults")]
    public ActionResult<IEnumerable<Vault>> GetVaultsByUserId(string id)
    {
      try
      {
        return Ok(_vservice.GetVaultsByUserId(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }
	}
}