﻿using Backend.Data;
using Backend.Interfaces.Form;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.Form
{
    public class FormRepository : IFormRepository
    {
        private readonly DataContext _context;

        public FormRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateForm(Models.Form.Form form)
        {
            _context.Add(form);
            return Save();
        }

        public bool DeleteForm(Models.Form.Form form)
        {
            _context.Remove(form);
            return Save();
        }

        public bool FormExists(int id)
        {
            return _context.Forms.Any(m => m.Id == id);
        }

        public bool FormExists(string title)
        {
            return _context.Forms.Any(m => m.Title == title);
        }

        public Models.Form.Form GetForm(int id)
        {
            return _context.Forms.Where(m => m.Id == id).FirstOrDefault();
        }

        public Models.Form.Form GetForm(string title)
        {
            return _context.Forms.Where(m => m.Title == title).FirstOrDefault();
        }

        public int GetFormCount()
        {
            return _context.Forms.Count();
        }

        public ICollection<Models.Form.Form> GetForms()
        {
            return _context.Forms.ToList();
        }

        public ICollection<Models.Form.Form> GetProjectForms(int id)
        {
            return _context.Forms.Where(m => m.ProjectId == id).ToList();
        }

        public bool ProjectFormExists(int id)
        {
            return _context.Forms.Any(m => m.ProjectId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateForm(Models.Form.Form form)
        {
            _context.Update(form);
            return Save();
        }

        public ICollection<Models.Form.Form> GetFormsWithResponsesByProjectId(int projectId)
        {
            return _context.Forms
                .Include(f => f.FormResponses)
                .Where(f => f.ProjectId == projectId)
                .ToList();
        }

        public ICollection<Models.Form.Form> GetFormsByProjectIdWithResponses(int projectId)
        {
            return _context.Forms
                .Include(f => f.FormResponses)
                .Where(f => f.ProjectId == projectId && f.FormResponses.Any())
                .ToList();
        }
    }
}
