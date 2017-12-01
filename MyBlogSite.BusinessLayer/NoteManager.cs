using MyBlogSite.BusinessLayer;
using MyBlogSite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog02.WebApp
{
    public class NoteManager
    {
        private Repository<Note> repo_note = new Repository<Note>();

        public List<Note> GetAllNote()
        {

            return repo_note.List();
        }
        public IQueryable<Note> GetAllNoteQueryable()
        {

            return repo_note.ListQueryable();
        }
    }
}