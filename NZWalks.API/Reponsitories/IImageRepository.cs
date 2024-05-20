﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Reponsitories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
