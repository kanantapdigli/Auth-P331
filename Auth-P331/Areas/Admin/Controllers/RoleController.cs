﻿using Auth_P331.Areas.Admin.ViewModels.Role;
using Auth_P331.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Auth_P331.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
	public class RoleController : Controller
	{
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
		{
            _roleManager = roleManager;
        }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var model = new RoleIndexVM
			{
				Roles = await _roleManager.Roles.Where(r => r.Name != UserRoles.SuperAdmin.ToString()).ToListAsync()
			};

			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(RoleCreateVM model)
		{
			if (!ModelState.IsValid) return View(model);

			var role = await _roleManager.FindByNameAsync(model.Name);
			if (role is not null)
			{
				ModelState.AddModelError("Name", "Bu adda rol artıq mövcuddur");
				return View();
			}

			role = new IdentityRole
			{
				Name = model.Name
			};

			var result = await _roleManager.CreateAsync(role);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);

				return View();
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Update(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role is null) return NotFound();

			var model = new RoleUpdateVM
			{
				Name = role.Name
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Update(string id, RoleUpdateVM model)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role is null) return NotFound();

			if (await _roleManager.Roles.AnyAsync(r => r.Name == model.Name))
			{
				ModelState.AddModelError("Name", "Bu adda rol mövcuddur");
				return View();
			}

			role.Name = model.Name;

			await _roleManager.UpdateAsync(role);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role is null) return NotFound();

			var result = await _roleManager.DeleteAsync(role);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
					throw new Exception(error.Description);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
