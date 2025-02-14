using System;
using Microsoft.EntityFrameworkCore;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data;

public class UnitOfWork(NikolaContext context) : IUnitOfWork
{
    internal NikolaContext _context = context;
    private IRepository<int, Artist>? _artistRepository;
    private IRepository<int, Album>? _albumRepository;
    private IRepository<int, Song>? _songRepository;
    private bool _disposed = false;

    public IRepository<int, Artist> ArtistRepository
    {
        get
        {
            _artistRepository ??= new Repository<int, Artist>(_context);
            return _artistRepository;
        }
    }

    public IRepository<int, Album> AlbumRepository
    {
        get
        {
            _albumRepository ??= new Repository<int, Album>(_context);
            return _albumRepository;
        }
    }

    public IRepository<int, Song> SongRepository
    {
        get
        {
            _songRepository ??= new Repository<int, Song>(_context);
            return _songRepository;
        }
    }

    public async Task SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            ex.Entries.Single().Reload();

        }
    }

    #region IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.DisposeAsync();
            }
        }
        _disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
    }

    #endregion
}