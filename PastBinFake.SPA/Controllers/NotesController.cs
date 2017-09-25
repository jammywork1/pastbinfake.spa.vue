using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PastBinFake.SPA.DomainLogic;
using PastBinFake.SPA.DomainLogic.DataTransferObjects;
using PastBinFake.SPA.DomainLogic.Exceptions;
using PastBinFake.SPA.ViewModels;
using Serilog;

namespace PastBinFake.SPA
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly SystemFacade _systemFacade;

        public NotesController(SystemFacade systemFacade, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _systemFacade = systemFacade ?? throw new ArgumentNullException(nameof(systemFacade));
        }

        [HttpGet]
        public async Task<IEnumerable<NoteShortViewModel>> Get()
        {
            Log.Logger.Debug($"Call get all notes");
            var founds = await _systemFacade.GetNotesSlice(int.MaxValue, 0);
            return founds.Select(_mapper.Map<NoteDTO, NoteShortViewModel>);
        }

        [HttpGet("{url}")]
        public async Task<ActionResult> Get(string url)
        {
            Log.Logger.Warning($"Call get note by url {url}");
            try
            {
                var found = await _systemFacade.GetNoteByUrl(url);
                Log.Logger.Warning("Found note {@found}", found);
                Log.Logger.Warning("Found note {found}", found);
                return StatusCode(200, _mapper.Map<NoteDTO, NoteViewModel>(found));
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "Not found");
            }
            catch (NoteExpiredException)
            {
                return StatusCode(423, "Note expired");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NoteCreateViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            var dto = _mapper.Map<NoteCreateViewModel, NoteCreateDTO>(viewModel);
            var createdNote = await _systemFacade.CreateNote(dto);
            return StatusCode(200, _mapper.Map<NoteDTO, NoteViewModel>(createdNote));
        }
    }
}