using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using keepr.Services;

namespace keepr.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VaultKeepsController : ControllerBase
	{
		private readonly VaultKeepsService _service;

		public VaultKeepsController(VaultKeepsService service)
		{
			_service = service;
		}

		[HttpGet]
		[Authorize]

		public ActionResult<IEnumerable<VaultKeep>> GetAll()
		{
			try
			{
				return Ok(_service.Get());
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vk)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				vk.CreatorId = userInfo.Id;
				_service.Create(vk, userInfo.Id);
				return Ok(vk);
			}
			catch (System.Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult<string>> Delete(int id)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				_service.Delete(id, userInfo.Id);
				return Ok("success");
			}
			catch (System.Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
