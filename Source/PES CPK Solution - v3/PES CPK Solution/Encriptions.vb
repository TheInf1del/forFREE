Imports System.IO
Imports System.IO.Compression

Module Encriptions
    Function Compress(ByVal toCompress As Byte()) As Byte()
        Using inputStream As MemoryStream = New MemoryStream(toCompress)
            Using outputStream As MemoryStream = New MemoryStream()
                Using compressionStream As DeflateStream =
                    New DeflateStream(outputStream, CompressionMode.Compress)
                    inputStream.CopyTo(compressionStream)
                End Using
                Compress = outputStream.ToArray()
            End Using
        End Using
    End Function

    Function Decompress(ByVal toDecompress As Byte()) As Byte()
        Using inputStream As MemoryStream = New MemoryStream(toDecompress)
            Using outputStream As MemoryStream = New MemoryStream()
                Using decompressionStream As DeflateStream =
                    New DeflateStream(inputStream, CompressionMode.Decompress)
                    decompressionStream.CopyTo(outputStream)
                End Using
                Decompress = outputStream.ToArray
            End Using
        End Using
    End Function
End Module
