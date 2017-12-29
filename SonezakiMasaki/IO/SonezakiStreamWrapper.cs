// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace SonezakiMasaki.IO
{
    internal sealed class SonezakiStreamWrapper : Stream
    {
        readonly Stack<CompoundingHash> _compoundingHashes = new Stack<CompoundingHash>();
        Stream _stream;
        bool _isDisposed;

        public SonezakiStreamWrapper( Stream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        /// <inheritdoc />
        public override bool CanRead => _stream.CanRead;

        /// <inheritdoc />
        public override bool CanSeek => false;

        /// <inheritdoc />
        public override bool CanWrite => _stream.CanWrite;

        /// <inheritdoc />
        public override long Length => _stream.Length;

        /// <inheritdoc />
        public override long Position
        {
            get => _stream.Position;
            set => _stream.Position = value;
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public void PushCompoundingHash( CompoundingHash hash )
        {
            if ( hash == null )
            {
                throw new ArgumentNullException( nameof( hash ) );
            }

            _compoundingHashes.Push( hash );
        }

        public CompoundingHash PopCompoundingHash()
        {
            if ( _compoundingHashes.Count == 0 )
            {
                throw new InvalidOperationException( "There are no hashes on the stack." );
            }

            return _compoundingHashes.Pop();
        }

        public override int Read( byte[] buffer, int offset, int count )
        {
            int numBytesRead = _stream.Read( buffer, offset, count );

            foreach ( CompoundingHash hash in _compoundingHashes )
            {
                hash.AddBytes( buffer, numBytesRead );
            }

            return numBytesRead;
        }

        public override int ReadByte()
        {
            return _stream.ReadByte();
        }

        public override long Seek( long offset, SeekOrigin origin )
        {
            throw new NotSupportedException();
        }

        public override void SetLength( long value )
        {
            _stream.SetLength( value );
        }

        public override void Write( byte[] buffer, int offset, int count )
        {
            _stream.Write( buffer, offset, count );

            foreach ( CompoundingHash hash in _compoundingHashes )
            {
                hash.AddBytes( buffer, count );
            }
        }

        public override void WriteByte( byte value )
        {
            _stream.WriteByte( value );
        }

        protected override void Dispose( bool disposing )
        {
            if ( _isDisposed )
            {
                return;
            }

            _isDisposed = true;

            try
            {
                DisposeStreamWrapper( disposing );
            }
            finally
            {
                base.Dispose( disposing );
            }
        }

        void DisposeStreamWrapper( bool disposing )
        {
            if ( disposing )
            {
                // Managed resources
                if ( _stream != null )
                {
                    _stream.Dispose();
                    _stream = null;
                }
            }

            // Unmanaged resources
        }
    }
}
