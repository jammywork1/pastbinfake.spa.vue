using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PastBinFake.SPA.DataAccessLayer;
using PastBinFake.SPA.DataAccessLayer.Entities;
using PastBinFake.SPA.DataAccessLayer.Exceptions;
using PastBinFake.SPA.DomainLogic.DataTransferObjects;
using PastBinFake.SPA.DomainLogic.Exceptions;
using PastBinFake.SPA.DomainLogic.Models;

namespace PastBinFake.SPA.DomainLogic
{
    public class NotesService
    {
        private readonly IMapper _mapper;
        private readonly IShortUrlGenerator _shortUrlGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public NotesService(IUnitOfWork unitOfWork, IMapper mapper, IShortUrlGenerator shortUrlGenerator)
        {
            _shortUrlGenerator = shortUrlGenerator ?? throw new ArgumentNullException(nameof(shortUrlGenerator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <exception cref="NotFoundException">Throw if note not found</exception>
        /// <exception cref="NoteExpiredException">Throw if note expired</exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<NoteDTO> GetByUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(url));
            try
            {
                var found = await _unitOfWork.NoteRepository.GetNoteByUrl(url);
                if (found.ExpiredDateTime.HasValue && found.ExpiredDateTime.Value < DateTime.Now)
                    throw new NoteExpiredException();
                return _mapper.Map<INoteEntity, NoteDTO>(found);
            }
            catch (EntityNotFoundException)
            {
                throw new NotFoundException();
            }
        }

        /// <summary>
        /// Method returns a slice of all not expired notes skipped <c>skip</c> and taked <c>take</c>
        /// </summary>
        /// <param name="take">Count of returned notes</param>
        /// <param name="skip">Count of skipped notes at first</param>
        public async Task<IEnumerable<NoteDTO>> GetSlice(int take, int skip)
        {
            if (take <= 0) throw new ArgumentOutOfRangeException(nameof(take));
            if (skip < 0) throw new ArgumentOutOfRangeException(nameof(skip));
            var slice = await _unitOfWork.NoteRepository.GetActualNotesSlice(take, skip);
            return slice.Select(_mapper.Map<INoteEntity, NoteDTO>);
        }

        /// <exception cref="CreateNoteException">Throw if create error</exception>
        public async Task<NoteDTO> CreateNote(NoteCreateDTO noteCreate)
        {
            if (noteCreate == null) throw new ArgumentNullException(nameof(noteCreate));
            var entity = _mapper.Map<NoteCreateDTO, NoteCreateDomainModel>(noteCreate);
            entity.Url = _shortUrlGenerator.Generate();
            entity.CreatedDateTime = DateTime.Now;
            entity.ExpiredDateTime = DurationCalculator.Calculate(DateTime.Now, noteCreate.Duration);
            try
            {
                await _unitOfWork.NoteRepository.Create(entity);
            }
            catch (CreateEntityException)
            {
                throw new CreateNoteException();
            }
            return _mapper.Map<NoteCreateDomainModel, NoteDTO>(entity);
        }
    }
}