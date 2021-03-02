// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using CodeWorks.Auth0Provider;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using keepr.Models;
// using keepr.Services;

// namespace keepr.Controllers
// {
// 	public class VaultKeepsController : ControllerBase
// 	{
// 		private readonly VaultKeepsService _service;
// 		private readonly VaultsService _vservice;
// 		private readonly KeepsService _kservice;

// 		public VaultKeepsController(VaultKeepsService service)
// 		{
// 			_service = service;
// 		}

// 		//REVIEW[epic=many-to-many] Which methods are actually needed here?
// 		[HttpPost]
// 		[Authorize]
// 		public async Task<ActionResult<string>> Create([FromBody] VaultKeep vk)
// 		{
// 			try
// 			{ 
// 				Vault v = await _vservice.GetById();
// 				Keep k = await _kservice.GetById();
// 				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
// 				_service.Create(vk, userInfo.Id);
// 				return Ok("success");
// 			}
// 			catch (System.Exception e)
// 			{
// 				return BadRequest(e.Message);
// 			}
// 		}

// 		// Delete 
// 		[HttpDelete("{id}")]
// 		public ActionResult<string> Delete(int id)
// 		{
// 			try
// 			{
// 				_service.Delete(id);
// 				return Ok("success");
// 			}
// 			catch (System.Exception e)
// 			{
// 				return BadRequest(e.Message);
// 			}
// 		}
// 	}
// }
