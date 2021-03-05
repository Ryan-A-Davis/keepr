using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using keepr.Services;
using keepr.Exceptions;
using Microsoft.AspNetCore.Http;

namespace keepr.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VaultsController : ControllerBase
	{
		private readonly VaultsService _vs;
		private readonly KeepsService _ks;
		public VaultsController(VaultsService vs, KeepsService ks)
		{
			_vs = vs;
			_ks = ks;
		}

		[HttpGet("{id}")]

		public async Task<ActionResult<Vault>> Get(int id)
		{

			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();

				return Ok(_vs.Get(id, userInfo?.Id));
			}
			catch (NotAuthorized e)
			{
				return StatusCode(StatusCodes.Status403Forbidden, e.Message);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}/keeps")]
		public async Task<ActionResult<Vault>> GetKeepsByVaultId(int id)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				return Ok(_ks.GetByVaultId(id, userInfo?.Id));
			}
			catch (NotAuthorized e)
			{
				//NOTE DO this  to all forbiddens
				return StatusCode(StatusCodes.Status403Forbidden, e.Message);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Vault>> Post([FromBody] Vault newVault)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				newVault.Creator = userInfo;
				newVault.CreatorId = userInfo.Id;
				Vault created = _vs.Create(newVault);
				return Ok(created);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault editData)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				editData.Id = id;
				editData.Creator = userInfo;
				editData.CreatorId = userInfo.Id;
				return Ok(_vs.Edit(editData, userInfo.Id));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}/keeps")]
		[Authorize]
		public async Task<ActionResult<Vault>> addKeep(int id, [FromBody] Vault editData)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				editData.Id = id;
				editData.Creator = userInfo;
				editData.CreatorId = userInfo.Id;
				return Ok(_vs.Edit(editData, userInfo.Id));
			}
			catch (Exception e)
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
				return Ok(_vs.Delete(id, userInfo.Id));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

	}
}