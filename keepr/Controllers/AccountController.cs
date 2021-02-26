using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using keepr.Services;

namespace keepr.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class AccountController : ControllerBase
	{
		private readonly ProfilesService _ps;


		public AccountController(ProfilesService ps)
		{
			_ps = ps;

		}

		[HttpGet]
		public async Task<ActionResult<Profile>> Get()
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				return Ok(_ps.GetOrCreateProfile(userInfo));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// [HttpGet("keeps")]
		// public async Task<ActionResult<IEnumerable<Keep>>> GetKeepsByProfileIdAsync()
		// {
		// 	try
		// 	{
		// 		Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
		// 		IEnumerable<Keep> keeps = _ks.GetKeepsByAccountId(userInfo.Id);
		// 		return Ok(keeps);
		// 	}
		// 	catch (Exception e)
		// 	{
		// 		return BadRequest(e.Message);
		// 	}
		// }
	}
}