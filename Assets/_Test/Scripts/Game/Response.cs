using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.Game
{
    [Serializable]
    public class Response
    {
        public List<ResponseResult> results;

        public Response(List<ResponseResult> results)
        {
            this.results = results;
        }

        public string Name => results.First().name.first;
        public string SpriteURL => results.First().picture.large;
    }

    [Serializable]
    public class ResponseResult
    {
        public NameResult name;
        public PictureResult picture;

        public ResponseResult(NameResult name, PictureResult picture)
        {
            this.name = name;
            this.picture = picture;
        }
    }

    [Serializable]
    public class PictureResult
    {
        public string large;

        public PictureResult(string large)
        {
            this.large = large;
        }
    }

    [Serializable]
    public class NameResult
    {
        public string first;

        public NameResult(string first)
        {
            this.first = first;
        }
    }

}
