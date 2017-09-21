using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastBinFake.SPA.DomainLogic.DataTransferObjects;

namespace PastBinFake.SPA.DomainLogic
{
    public class SystemFacade
    {
        private readonly NotesService _notesService;

        public SystemFacade(NotesService notesService)
        {
            _notesService = notesService ?? throw new ArgumentNullException(nameof(notesService));
        }

        public async Task<NoteDTO> CreateNote(NoteCreateDTO noteCreate)
        {
            if (noteCreate == null) throw new ArgumentNullException(nameof(noteCreate));
            return await _notesService.CreateNote(noteCreate);
        }

        
        public async Task<NoteDTO> GetNoteByUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(url));
            var found = await _notesService.GetByUrl(url);
            return found;
        }

        public async Task<IEnumerable<NoteDTO>> GetNotesSlice(int take, int skip)
        {
            if (take <= 0) throw new ArgumentOutOfRangeException(nameof(take));
            if (skip < 0) throw new ArgumentOutOfRangeException(nameof(skip));
            
            return await _notesService.GetSlice(take, skip);
        }
    }
}