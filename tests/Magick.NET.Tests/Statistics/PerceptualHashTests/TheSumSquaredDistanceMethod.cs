﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using NSubstitute;
using Xunit;

namespace Magick.NET.Tests;

public partial class PerceptualHashTests
{
    public class TheSumSquaredDistanceMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCustomImplementationDoesNotHaveExpectedChannels()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();
            Assert.NotNull(phash);

            var perceptualHash = Substitute.For<IPerceptualHash>();
            perceptualHash.GetChannel(PixelChannel.Blue).Returns((IChannelPerceptualHash?)null);

            var exception = Assert.Throws<NotSupportedException>(() => phash.SumSquaredDistance(perceptualHash));
            Assert.Equal("The other perceptual hash should contain a red, green and blue channel.", exception.Message);
        }

        [Fact]
        public void ShouldReturnTheDifference()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();
            Assert.NotNull(phash);

            using var other = new MagickImage(Files.MagickNETIconPNG);
            other.HasAlpha = false;

            Assert.Equal(3U, other.ChannelCount);

            var otherPhash = other.PerceptualHash();
            Assert.NotNull(otherPhash);

#if Q8
            Assert.Equal(821.09, phash.SumSquaredDistance(otherPhash), 2);
#elif Q16
            Assert.Equal(851.76, phash.SumSquaredDistance(otherPhash), 2);
#else
            Assert.Equal(832.24, phash.SumSquaredDistance(otherPhash), 2);
#endif
        }
    }
}
