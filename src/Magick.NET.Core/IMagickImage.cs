﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageMagick.Drawing;

namespace ImageMagick;

/// <summary>
/// Interface that represents an ImageMagick image.
/// </summary>
public partial interface IMagickImage : IMagickImageCreateOperations, IDisposable
{
    /// <summary>
    /// Event that will be raised when progress is reported by this image.
    /// </summary>
    event EventHandler<ProgressEventArgs> Progress;

    /// <summary>
    /// Event that will we raised when a warning is raised by ImageMagick.
    /// </summary>
    event EventHandler<WarningEventArgs> Warning;

    /// <summary>
    /// Gets or sets the time in 1/100ths of a second which must expire before splaying the next image in an
    /// animated sequence.
    /// </summary>
    uint AnimationDelay { get; set; }

    /// <summary>
    /// Gets or sets the number of iterations to loop an animation (e.g. Netscape loop extension) for.
    /// </summary>
    uint AnimationIterations { get; set; }

    /// <summary>
    /// Gets or sets the ticks per seconds for the animation delay.
    /// </summary>
    int AnimationTicksPerSecond { get; set; }

    /// <summary>
    /// Gets the names of the artifacts.
    /// </summary>
    IEnumerable<string> ArtifactNames { get; }

    /// <summary>
    /// Gets the names of the attributes.
    /// </summary>
    IEnumerable<string> AttributeNames { get; }

    /// <summary>
    /// Gets the height of the image before transformations.
    /// </summary>
    uint BaseHeight { get; }

    /// <summary>
    /// Gets the width of the image before transformations.
    /// </summary>
    uint BaseWidth { get; }

    /// <summary>
    /// Gets or sets a value indicating whether black point compensation should be used.
    /// </summary>
    bool BlackPointCompensation { get; set; }

    /// <summary>
    /// Gets the smallest bounding box enclosing non-border pixels. The current fuzz value is used
    /// when discriminating between pixels.
    /// </summary>
    IMagickGeometry? BoundingBox { get; }

    /// <summary>
    /// Gets the number of channels that the image contains.
    /// </summary>
    uint ChannelCount { get; }

    /// <summary>
    /// Gets the channels of the image.
    /// </summary>
    IEnumerable<PixelChannel> Channels { get; }

    /// <summary>
    /// Gets or sets the chromaticity of the image.
    /// </summary>
    IChromaticityInfo Chromaticity { get; set; }

    /// <summary>
    /// Gets or sets the image class (DirectClass or PseudoClass)
    /// NOTE: Setting a DirectClass image to PseudoClass will result in the loss of color information
    /// if the number of colors in the image is greater than the maximum palette size (either 256 (Q8)
    /// or 65536 (Q16).
    /// </summary>
    ClassType ClassType { get; set; }

    /// <summary>
    /// Gets or sets the distance where colors are considered equal.
    /// </summary>
    Percentage ColorFuzz { get; set; }

    /// <summary>
    /// Gets or sets the colormap size (number of colormap entries).
    /// </summary>
    int ColormapSize { get; set; }

    /// <summary>
    /// Gets or sets the color space of the image.
    /// </summary>
    ColorSpace ColorSpace { get; set; }

    /// <summary>
    /// Gets or sets the color type of the image.
    /// </summary>
    ColorType ColorType { get; set; }

    /// <summary>
    /// Gets or sets the comment text of the image.
    /// </summary>
    string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the composition operator to be used when composition is implicitly used (such as for image flattening).
    /// </summary>
    CompositeOperator Compose { get; set; }

    /// <summary>
    /// Gets the compression method of the image.
    /// </summary>
    CompressionMethod Compression { get; }

    /// <summary>
    /// Gets or sets the vertical and horizontal resolution in pixels of the image.
    /// </summary>
    Density Density { get; set; }

    /// <summary>
    /// Gets or sets the depth (bits allocated to red/green/blue components).
    /// </summary>
    uint Depth { get; set; }

    /// <summary>
    /// Gets or sets the endianness (little like Intel or big like SPARC) for image formats which support
    /// endian-specific options.
    /// </summary>
    Endian Endian { get; set; }

    /// <summary>
    /// Gets the original file name of the image (only available if read from disk).
    /// </summary>
    string? FileName { get; }

    /// <summary>
    /// Gets or sets the filter to use when resizing image.
    /// </summary>
    FilterType FilterType { get; set; }

    /// <summary>
    /// Gets or sets the format of the image.
    /// </summary>
    MagickFormat Format { get; set; }

    /// <summary>
    /// Gets the gamma level of the image.
    /// </summary>
    double Gamma { get; }

    /// <summary>
    /// Gets or sets the gif disposal method.
    /// </summary>
    GifDisposeMethod GifDisposeMethod { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the image supports transparency (alpha channel).
    /// </summary>
    bool HasAlpha { get; set; }

    /// <summary>
    /// Gets the height of the image.
    /// </summary>
    uint Height { get; }

    /// <summary>
    /// Gets the type of interlacing to use.
    /// </summary>
    Interlace Interlace { get; }

    /// <summary>
    /// Gets or sets the pixel color interpolate method to use.
    /// </summary>
    PixelInterpolateMethod Interpolate { get; set; }

    /// <summary>
    /// Gets a value indicating whether none of the pixels in the image have an alpha value other
    /// than OpaqueAlpha (QuantumRange).
    /// </summary>
    bool IsOpaque { get; }

    /// <summary>
    /// Gets or sets the label of the image.
    /// </summary>
    string? Label { get; set; }

    /// <summary>
    /// Gets or sets the number of meta channels that the image contains.
    /// </summary>
    uint MetaChannelCount { get; set; }

    /// <summary>
    /// Gets or sets the photo orientation of the image.
    /// </summary>
    OrientationType Orientation { get; set; }

    /// <summary>
    /// Gets or sets the preferred size and location of an image canvas.
    /// </summary>
    IMagickGeometry Page { get; set; }

    /// <summary>
    /// Gets the names of the profiles.
    /// </summary>
    IEnumerable<string> ProfileNames { get; }

    /// <summary>
    /// Gets or sets the JPEG/MIFF/PNG compression level (default 75).
    /// </summary>
    uint Quality { get; set; }

    /// <summary>
    /// Gets or sets the type of rendering intent.
    /// </summary>
    RenderingIntent RenderingIntent { get; set; }

    /// <summary>
    /// Gets the signature of this image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string Signature { get; }

    /// <summary>
    /// Gets the number of colors in the image.
    /// </summary>
    uint TotalColors { get; }

    /// <summary>
    /// Gets or sets the virtual pixel method.
    /// </summary>
    VirtualPixelMethod VirtualPixelMethod { get; set; }

    /// <summary>
    /// Gets the width of the image.
    /// </summary>
    uint Width { get; }

    /// <summary>
    /// Applies the specified alpha option.
    /// </summary>
    /// <param name="value">The option to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Alpha(AlphaOption value);

    /// <summary>
    /// Annotate using specified text, and bounding area.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Annotate(string text, IMagickGeometry boundingArea);

    /// <summary>
    /// Annotate using specified text, bounding area, and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Annotate(string text, IMagickGeometry boundingArea, Gravity gravity);

    /// <summary>
    /// Annotate using specified text, bounding area, and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="angle">The rotation.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Annotate(string text, IMagickGeometry boundingArea, Gravity gravity, double angle);

    /// <summary>
    /// Annotate with text (bounding area is entire image) and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Annotate(string text, Gravity gravity);

    /// <summary>
    /// Extracts the 'mean' from the image and adjust the image to try make set its gamma appropriatally.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoGamma();

    /// <summary>
    /// Extracts the 'mean' from the image and adjust the image to try make set its gamma appropriatally.
    /// </summary>
    /// <param name="channels">The channel(s) to set the gamma for.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoGamma(Channels channels);

    /// <summary>
    /// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
    /// to the full quantum range.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoLevel();

    /// <summary>
    /// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
    /// to the full quantum range.
    /// </summary>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoLevel(Channels channels);

    /// <summary>
    /// Automatically selects a threshold and replaces each pixel in the image with a black pixel if
    /// the image intentsity is less than the selected threshold otherwise white.
    /// </summary>
    /// <param name="method">The threshold method to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoThreshold(AutoThresholdMethod method);

    /// <summary>
    /// Forces all pixels below the threshold into black while leaving all pixels at or above
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BlackThreshold(Percentage threshold);

    /// <summary>
    /// Forces all pixels below the threshold into black while leaving all pixels at or above
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <param name="channels">The channel(s) to make black.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BlackThreshold(Percentage threshold, Channels channels);

    /// <summary>
    /// Changes the brightness and/or contrast of an image. It converts the brightness and
    /// contrast parameters into slope and intercept and calls a polynomical function to apply
    /// to the image.
    /// </summary>
    /// <param name="brightness">The brightness.</param>
    /// <param name="contrast">The contrast.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BrightnessContrast(Percentage brightness, Percentage contrast);

    /// <summary>
    /// Changes the brightness and/or contrast of an image. It converts the brightness and
    /// contrast parameters into slope and intercept and calls a polynomical function to apply
    /// to the image.
    /// </summary>
    /// <param name="brightness">The brightness.</param>
    /// <param name="contrast">The contrast.</param>
    /// <param name="channels">The channel(s) that should be changed.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BrightnessContrast(Percentage brightness, Percentage contrast, Channels channels);

    /// <summary>
    /// A variant of adaptive histogram equalization in which the contrast amplification is limited,
    /// so as to reduce this problem of noise amplification.
    /// </summary>
    /// <param name="xTiles">The percentage of tile divisions to use in horizontal direction.</param>
    /// <param name="yTiles">The percentage of tile divisions to use in vertical direction.</param>
    /// <param name="numberBins">The number of bins for histogram ("dynamic range").</param>
    /// <param name="clipLimit">The contrast limit for localised changes in contrast. A limit less than 1
    /// results in standard non-contrast limited AHE.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clahe(Percentage xTiles, Percentage yTiles, uint numberBins, double clipLimit);

    /// <summary>
    /// A variant of adaptive histogram equalization in which the contrast amplification is limited,
    /// so as to reduce this problem of noise amplification.
    /// </summary>
    /// <param name="xTiles">The number of tile divisions to use in horizontal direction.</param>
    /// <param name="yTiles">The number of tile divisions to use in vertical direction.</param>
    /// <param name="numberBins">The number of bins for histogram ("dynamic range").</param>
    /// <param name="clipLimit">The contrast limit for localised changes in contrast. A limit less than 1
    /// results in standard non-contrast limited AHE.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clahe(uint xTiles, uint yTiles, uint numberBins, double clipLimit);

    /// <summary>
    /// Set each pixel whose value is below zero to zero and any the pixel whose value is above
    /// the quantum range to the quantum range (Quantum.Max) otherwise the pixel value
    /// remains unchanged.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clamp();

    /// <summary>
    /// Set each pixel whose value is below zero to zero and any the pixel whose value is above
    /// the quantum range to the quantum range (Quantum.Max) otherwise the pixel value
    /// remains unchanged.
    /// </summary>
    /// <param name="channels">The channel(s) to clamp.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clamp(Channels channels);

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect inside
    /// the clipping path.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clip();

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect inside
    /// the clipping path.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use
    /// clipping path numbered by name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clip(string pathName);

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect outside
    /// the clipping path.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ClipOutside();

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect outside
    /// the clipping path.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use
    /// clipping path numbered by name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ClipOutside(string pathName);

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clut(IMagickImage image);

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="channels">The channel(s) to clut.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clut(IMagickImage image, Channels channels);

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clut(IMagickImage image, PixelInterpolateMethod method);

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="channels">The channel(s) to clut.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clut(IMagickImage image, PixelInterpolateMethod method, Channels channels);

    /// <summary>
    /// Applies the color decision list from the specified ASC CDL file.
    /// </summary>
    /// <param name="fileName">The file to read the ASC CDL information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ColorDecisionList(string fileName);

    /// <summary>
    /// Compare current image with another image and returns error information.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <returns>The error information.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo Compare(IMagickImage image);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    double Compare(IMagickImage image, ErrorMetric metric);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    double Compare(IMagickImage image, ErrorMetric metric, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Channels channels);

    /// <summary>
    /// Compose an image onto another using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, CompositeOperator compose);

    /// <summary>
    /// Compose an image onto another using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, CompositeOperator compose, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, CompositeOperator compose, string? args);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, CompositeOperator compose, string? args, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, CompositeOperator compose);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, CompositeOperator compose, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, CompositeOperator compose, string? args);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, CompositeOperator compose, string? args, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose, string? args);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose, string? args, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose, string? args);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose, string? args, Channels channels);

    /// <summary>
    /// Contrast image (enhance intensity differences in image).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Contrast();

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ContrastStretch(Percentage blackPoint);

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ContrastStretch(Percentage blackPoint, Percentage whitePoint);

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <param name="channels">The channel(s) to constrast stretch.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ContrastStretch(Percentage blackPoint, Percentage whitePoint, Channels channels);

    /// <summary>
    /// Returns the convex hull points of an image canvas.
    /// </summary>
    /// <returns>The convex hull points of an image canvas.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IEnumerable<PointD> ConvexHull();

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source);

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, Channels channels);

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, IMagickGeometry geometry);

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, IMagickGeometry geometry, Channels channels);

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="x">The X offset to copy the pixels to.</param>
    /// <param name="y">The Y offset to copy the pixels to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, IMagickGeometry geometry, int x, int y);

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="x">The X offset to copy the pixels to.</param>
    /// <param name="y">The Y offset to copy the pixels to.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, IMagickGeometry geometry, int x, int y, Channels channels);

    /// <summary>
    /// Displaces an image's colormap by a given number of positions.
    /// </summary>
    /// <param name="amount">The amount to displace the colormap.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CycleColormap(int amount);

    /// <summary>
    /// Converts cipher pixels to plain pixels.
    /// </summary>
    /// <param name="passphrase">The password that was used to encrypt the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Decipher(string passphrase);

    /// <summary>
    /// Determines the bit depth (bits allocated to red/green/blue components). Use the Depth
    /// property to get the current value.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The bit depth (bits allocated to red/green/blue components).</returns>
    uint DetermineBitDepth();

    /// <summary>
    /// Determines the bit depth (bits allocated to red/green/blue components) of the specified channel.
    /// </summary>
    /// <param name="channels">The channel to get the depth for.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The bit depth (bits allocated to red/green/blue components) of the specified channel.</returns>
    uint DetermineBitDepth(Channels channels);

    /// <summary>
    /// Determines the color type of the image. This method can be used to automatically make the
    /// type GrayScale.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The color type of the image.</returns>
    ColorType DetermineColorType();

    /// <summary>
    /// Draw on image using one or more drawables.
    /// </summary>
    /// <param name="drawables">The drawable(s) to draw on the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Draw(params IDrawable[] drawables);

    /// <summary>
    /// Draw on image using a collection of drawables.
    /// </summary>
    /// <param name="drawables">The drawables to draw on the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Draw(IEnumerable<IDrawable> drawables);

    /// <summary>
    /// Converts pixels to cipher-pixels.
    /// </summary>
    /// <param name="passphrase">The password that to encrypt the image with.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Encipher(string passphrase);

    /// <summary>
    /// Applies a histogram equalization to the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Equalize();

    /// <summary>
    /// Applies a histogram equalization to the image.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Equalize(Channels channels);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateFunction">The function to use.</param>
    /// <param name="arguments">The arguments for the function.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, EvaluateFunction evaluateFunction, params double[] arguments);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateOperator">The operator to use.</param>
    /// <param name="value">The value to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, EvaluateOperator evaluateOperator, double value);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateOperator">The operator to use.</param>
    /// <param name="percentage">The value to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, EvaluateOperator evaluateOperator, Percentage percentage);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="evaluateOperator">The operator to use.</param>
    /// <param name="value">The value to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, IMagickGeometry geometry, EvaluateOperator evaluateOperator, double value);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="evaluateOperator">The operator to use.</param>
    /// <param name="percentage">The value to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, IMagickGeometry geometry, EvaluateOperator evaluateOperator, Percentage percentage);

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    ITypeMetric? FontTypeMetrics(string text);

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <param name="ignoreNewlines">Specifies if newlines should be ignored.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    ITypeMetric? FontTypeMetrics(string text, bool ignoreNewlines);

    /// <summary>
    /// Formats the specified expression (more info can be found here: https://imagemagick.org/script/escape.php).
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>The result of the expression.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string? FormatExpression(string expression);

    /// <summary>
    /// Applies a mathematical expression to the image.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Fx(string expression);

    /// <summary>
    /// Applies a mathematical expression to the image.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <param name="channels">The channel(s) to apply the expression to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Fx(string expression, Channels channels);

    /// <summary>
    /// Gamma correct image.
    /// </summary>
    /// <param name="gamma">The image gamma.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void GammaCorrect(double gamma);

    /// <summary>
    /// Gamma correct image.
    /// </summary>
    /// <param name="gamma">The image gamma for the channel.</param>
    /// <param name="channels">The channel(s) to gamma correct.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void GammaCorrect(double gamma, Channels channels);

    /// <summary>
    /// Retrieve the 8bim profile from the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The 8bim profile from the image.</returns>
    IEightBimProfile? Get8BimProfile();

    /// <summary>
    /// Returns the value of the artifact with the specified name.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <returns>The value of the artifact with the specified name.</returns>
    string? GetArtifact(string name);

    /// <summary>
    /// Returns the value of a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>The value of a named image attribute.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string? GetAttribute(string name);

    /// <summary>
    /// Returns the default clipping path. Null will be returned if the image has no clipping path.
    /// </summary>
    /// <returns>The default clipping path. Null will be returned if the image has no clipping path.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string? GetClippingPath();

    /// <summary>
    /// Returns the clipping path with the specified name. Null will be returned if the image has no clipping path.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use clipping path numbered by name.</param>
    /// <returns>The clipping path with the specified name. Null will be returned if the image has no clipping path.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string? GetClippingPath(string pathName);

    /// <summary>
    /// Retrieve the color profile from the image.
    /// </summary>
    /// <returns>The color profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IColorProfile? GetColorProfile();

    /// <summary>
    /// Retrieve the exif profile from the image.
    /// </summary>
    /// <returns>The exif profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IExifProfile? GetExifProfile();

    /// <summary>
    /// Retrieve the iptc profile from the image.
    /// </summary>
    /// <returns>The iptc profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IIptcProfile? GetIptcProfile();

    /// <summary>
    /// Retrieve a named profile from the image.
    /// </summary>
    /// <param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
    /// <returns>A named profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IImageProfile? GetProfile(string name);

    /// <summary>
    /// Retrieve the xmp profile from the image.
    /// </summary>
    /// <returns>The xmp profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IXmpProfile? GetXmpProfile();

    /// <summary>
    /// Converts the colors in the image to gray.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Grayscale();

    /// <summary>
    /// Converts the colors in the image to gray.
    /// </summary>
    /// <param name="method">The pixel intensity method to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Grayscale(PixelIntensityMethod method);

    /// <summary>
    /// Apply a color lookup table (Hald CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void HaldClut(IMagickImage image);

    /// <summary>
    /// Apply a color lookup table (Hald CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="channels">The channel(s) to hald clut.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void HaldClut(IMagickImage image, Channels channels);

    /// <summary>
    /// Gets a value indicating whether a profile with the specified name already exists on the image.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    /// <returns>A value indicating whether a profile with the specified name already exists on the image.</returns>
    bool HasProfile(string name);

    /// <summary>
    /// Import pixels from the specified byte array.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ImportPixels(byte[] data, IPixelImportSettings settings);

    /// <summary>
    /// Import pixels from the specified byte array.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ImportPixels(byte[] data, uint offset, IPixelImportSettings settings);

    /// <summary>
    /// Inverse contrast image (diminish intensity differences in image).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseContrast();

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, Channels channels);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma, Channels channels);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use..</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseSigmoidalContrast(double contrast);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseSigmoidalContrast(double contrast, double midpoint);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <param name="channels">The channel(s) that should be adjusted.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseSigmoidalContrast(double contrast, double midpoint, Channels channels);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpointPercentage">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseSigmoidalContrast(double contrast, Percentage midpointPercentage);

    /// <summary>
    /// Applies k-means color reduction to an image. This is a colorspace clustering or segmentation technique.
    /// </summary>
    /// <param name="settings">The kmeans settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Kmeans(IKmeansSettings settings);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(Percentage blackPointPercentage, Percentage whitePointPercentage);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, Channels channels);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma, Channels channels);

    /// <summary>
    /// Discards any pixels below the black point and above the white point and levels the remaining pixels.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LinearStretch(Percentage blackPoint, Percentage whitePoint);

    /// <summary>
    /// Local contrast enhancement.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="strength">The strength of the blur mask.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LocalContrast(double radius, Percentage strength);

    /// <summary>
    /// Local contrast enhancement.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="strength">The strength of the blur mask.</param>
    /// <param name="channels">The channel(s) that should be changed.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LocalContrast(double radius, Percentage strength, Channels channels);

    /// <summary>
    /// Lower image (darken the edges of an image to give a 3-D lowered effect).
    /// </summary>
    /// <param name="size">The size of the edges.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Lower(uint size);

    /// <summary>
    /// Filter image by replacing each pixel component with the median color in a circular neighborhood.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void MedianFilter();

    /// <summary>
    /// Filter image by replacing each pixel component with the median color in a circular neighborhood.
    /// </summary>
    /// <param name="radius">The radius of the pixel neighborhood.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void MedianFilter(uint radius);

    /// <summary>
    /// Returns the points that form the minimum bounding box around the image foreground objects with
    /// the "Rotating Calipers" algorithm. he method also returns these properties: minimum-bounding-box:area,
    /// minimum-bounding-box:width, minimum-bounding-box:height, and minimum-bounding-box:angle.
    /// </summary>
    /// <returns>The points that form the minimum bounding box around the image foreground objects.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IEnumerable<PointD> MinimumBoundingBox();

    /// <summary>
    /// Modulate percent brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Modulate(Percentage brightness);

    /// <summary>
    /// Modulate percent saturation and brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <param name="saturation">The saturation percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Modulate(Percentage brightness, Percentage saturation);

    /// <summary>
    /// Modulate percent hue, saturation, and brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <param name="saturation">The saturation percentage.</param>
    /// <param name="hue">The hue percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Modulate(Percentage brightness, Percentage saturation, Percentage hue);

    /// <summary>
    /// Returns the normalized moments of one or more image channels.
    /// </summary>
    /// <returns>The normalized moments of one or more image channels.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMoments Moments();

    /// <summary>
    /// Negate colors in image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Negate();

    /// <summary>
    /// Negate colors in image for the specified channel.
    /// </summary>
    /// <param name="channels">The channel(s) that should be negated.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Negate(Channels channels);

    /// <summary>
    /// Negate the grayscale colors in image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void NegateGrayscale();

    /// <summary>
    /// Negate the grayscale colors in image for the specified channel.
    /// </summary>
    /// <param name="channels">The channel(s) that should be negated.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void NegateGrayscale(Channels channels);

    /// <summary>
    /// Normalize image (increase contrast by normalizing the pixel values to span the full range
    /// of color values).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Normalize();

    /// <summary>
    /// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
    /// multiple intensity levels.
    /// </summary>
    /// <param name="thresholdMap">A string containing the name of the threshold dither map to use,
    /// followed by zero or more numbers representing the number of color levels tho dither between.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void OrderedDither(string thresholdMap);

    /// <summary>
    /// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
    /// multiple intensity levels.
    /// </summary>
    /// <param name="thresholdMap">A string containing the name of the threshold dither map to use,
    /// followed by zero or more numbers representing the number of color levels tho dither between.</param>
    /// <param name="channels">The channel(s) to dither.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void OrderedDither(string thresholdMap, Channels channels);

    /// <summary>
    /// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
    /// otherwise the pixel value remains unchanged.
    /// </summary>
    /// <param name="epsilon">The epsilon threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Perceptible(double epsilon);

    /// <summary>
    /// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
    /// otherwise the pixel value remains unchanged.
    /// </summary>
    /// <param name="epsilon">The epsilon threshold.</param>
    /// <param name="channels">The channel(s) to perceptible.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Perceptible(double epsilon, Channels channels);

    /// <summary>
    /// Returns the perceptual hash of this image with the colorspaces <see cref="ColorSpace.XyY"/>
    /// and <see cref="ColorSpace.HSB"/>.
    /// </summary>
    /// <returns>The perceptual hash of this image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IPerceptualHash? PerceptualHash();

    /// <summary>
    /// Returns the perceptual hash of this image.
    /// </summary>
    /// <param name="colorSpaces">The colorspaces to get the perceptual hash for.</param>
    /// <returns>The perceptual hash of this image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IPerceptualHash? PerceptualHash(params ColorSpace[] colorSpaces);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data, uint offset, uint count);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(FileInfo file);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(Stream stream);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(string fileName);

    /// <summary>
    /// Simulates a polaroid picture.
    /// </summary>
    /// <param name="caption">The caption to put on the image.</param>
    /// <param name="angle">The angle of image.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Polaroid(string caption, double angle, PixelInterpolateMethod method);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Posterize(int levels);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="channels">The channel(s) to posterize.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Posterize(int levels, Channels channels);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="method">Dither method to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Posterize(int levels, DitherMethod method);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="method">Dither method to use.</param>
    /// <param name="channels">The channel(s) to posterize.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Posterize(int levels, DitherMethod method, Channels channels);

    /// <summary>
    /// Sets an internal option to preserve the color type.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void PreserveColorType();

    /// <summary>
    /// Quantize image (reduce number of colors).
    /// </summary>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The error information.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo? Quantize(IQuantizeSettings settings);

    /// <summary>
    /// Raise image (lighten the edges of an image to give a 3-D raised effect).
    /// </summary>
    /// <param name="size">The size of the edges.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Raise(int size);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="percentageLow">The low threshold.</param>
    /// <param name="percentageHigh">The high threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RandomThreshold(Percentage percentageLow, Percentage percentageHigh);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="percentageLow">The low threshold.</param>
    /// <param name="percentageHigh">The high threshold.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RandomThreshold(Percentage percentageLow, Percentage percentageHigh, Channels channels);

    /// <summary>
    /// Applies soft and hard thresholding.
    /// </summary>
    /// <param name="percentageLowBlack">Defines the minimum black threshold value.</param>
    /// <param name="percentageLowWhite">Defines the minimum white threshold value.</param>
    /// <param name="percentageHighWhite">Defines the maximum white threshold value.</param>
    /// <param name="percentageHighBlack">Defines the maximum black threshold value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RangeThreshold(Percentage percentageLowBlack, Percentage percentageLowWhite, Percentage percentageHighWhite, Percentage percentageHighBlack);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, uint offset, uint count);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, uint offset, uint count, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file, uint width, uint height);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(Stream stream);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(Stream stream, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName, uint width, uint height);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Reduce noise in image using a noise peak elimination filter.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReduceNoise();

    /// <summary>
    /// Reduce noise in image using a noise peak elimination filter.
    /// </summary>
    /// <param name="order">The order to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReduceNoise(uint order);

    /// <summary>
    /// Associates a mask with the image as defined by the specified region.
    /// </summary>
    /// <param name="region">The mask region.</param>
    void RegionMask(IMagickGeometry region);

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo Remap(IMagickImage image);

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo Remap(IMagickImage image, IQuantizeSettings settings);

    /// <summary>
    /// Removes the artifact with the specified name.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    void RemoveArtifact(string name);

    /// <summary>
    /// Removes the attribute with the specified name.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    void RemoveAttribute(string name);

    /// <summary>
    /// Removes the region mask of the image.
    /// </summary>
    void RemoveRegionMask();

    /// <summary>
    /// Remove a profile from the image.
    /// </summary>
    /// <param name="profile">The profile to remove.</param>
    public void RemoveProfile(IImageProfile profile);

    /// <summary>
    /// Remove a named profile from the image.
    /// </summary>
    /// <param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
    void RemoveProfile(string name);

    /// <summary>
    /// Removes the associated read mask of the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RemoveReadMask();

    /// <summary>
    /// Removes the associated write mask of the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RemoveWriteMask();

    /// <summary>
    /// Resets the page property of this image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ResetPage();

    /// <summary>
    /// Segment (coalesce similar image components) by analyzing the histograms of the color
    /// components and identifying units that are homogeneous with the fuzzy c-means technique.
    /// Also uses QuantizeColorSpace and Verbose image attributes.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Segment();

    /// <summary>
    /// Segment (coalesce similar image components) by analyzing the histograms of the color
    /// components and identifying units that are homogeneous with the fuzzy c-means technique.
    /// Also uses QuantizeColorSpace and Verbose image attributes.
    /// </summary>
    /// <param name="quantizeColorSpace">Quantize colorspace.</param>
    /// <param name="clusterThreshold">This represents the minimum number of pixels contained in
    /// a hexahedra before it can be considered valid (expressed as a percentage).</param>
    /// <param name="smoothingThreshold">The smoothing threshold eliminates noise in the second
    /// derivative of the histogram. As the value is increased, you can expect a smoother second
    /// derivative.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Segment(ColorSpace quantizeColorSpace, double clusterThreshold, double smoothingThreshold);

    /// <summary>
    /// Inserts the artifact with the specified name and value into the artifact tree of the image.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <param name="value">The value of the artifact.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetArtifact(string name, string value);

    /// <summary>
    /// Inserts the artifact with the specified name and value into the artifact tree of the image.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <param name="flag">The value of the artifact.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetArtifact(string name, bool flag);

    /// <summary>
    /// Lessen (or intensify) when adding noise to an image.
    /// </summary>
    /// <param name="attenuate">The attenuate value.</param>
    void SetAttenuate(double attenuate);

    /// <summary>
    /// Sets a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetAttribute(string name, string value);

    /// <summary>
    /// Sets a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="flag">The value of the attribute.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetAttribute(string name, bool flag);

    /// <summary>
    /// Set the bit depth (bits allocated to red/green/blue components).
    /// </summary>
    /// <param name="value">The depth.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetBitDepth(uint value);

    /// <summary>
    /// Set the bit depth (bits allocated to red/green/blue components) of the specified channel.
    /// </summary>
    /// <param name="value">The depth.</param>
    /// <param name="channels">The channel to set the depth for.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetBitDepth(uint value, Channels channels);

    /// <summary>
    /// Sets the default clipping path.
    /// </summary>
    /// <param name="value">The clipping path.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetClippingPath(string value);

    /// <summary>
    /// Sets the clipping path with the specified name.
    /// </summary>
    /// <param name="value">The clipping path.</param>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use clipping path numbered by name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetClippingPath(string value, string pathName);

    /// <summary>
    /// Gets the compression of the image. This method should only be used when the encoder uses the compression of the image. For
    /// most usecases Setting.Compression should be used instead.
    /// </summary>
    /// <param name="compression">The compression method.</param>
    void SetCompression(CompressionMethod compression);

    /// <summary>
    /// Set the specified profile of the image. If a profile with the same name already exists it will be overwritten.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetProfile(IImageProfile profile);

    /// <summary>
    /// Set the specified profile of the image. If a profile with the same name already exists it will be overwritten.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetProfile(IColorProfile profile);

    /// <summary>
    /// Set the specified profile of the image. If a profile with the same name already exists it will be overwritten.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <param name="mode">The color transformation mode.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetProfile(IColorProfile profile, ColorTransformMode mode);

    /// <summary>
    /// Sets the associated read mask of the image. The mask must be the same dimensions as the image and
    /// only contain the colors black and white.
    /// </summary>
    /// <param name="image">The image that contains the read mask.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetReadMask(IMagickImage image);

    /// <summary>
    /// Sets the associated write mask of the image. The mask must be the same dimensions as the image and
    /// only contains the colors black and white or have grayscale values that will cause blended updates of
    /// the image.
    /// </summary>
    /// <param name="image">The image that contains the write mask.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetWriteMask(IMagickImage image);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SigmoidalContrast(double contrast);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SigmoidalContrast(double contrast, double midpoint);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <param name="channels">The channel(s) that should be adjusted.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SigmoidalContrast(double contrast, double midpoint, Channels channels);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpointPercentage">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SigmoidalContrast(double contrast, Percentage midpointPercentage);

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Solarize();

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process).
    /// </summary>
    /// <param name="factor">The factor to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Solarize(double factor);

    /// <summary>
    /// Sort pixels within each scanline in ascending order of intensity.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SortPixels();

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process).
    /// </summary>
    /// <param name="factorPercentage">The factor to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Solarize(Percentage factorPercentage);

    /// <summary>
    /// Returns the image statistics.
    /// </summary>
    /// <returns>The image statistics.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IStatistics Statistics();

    /// <summary>
    /// Returns the image statistics.
    /// </summary>
    /// <returns>The image statistics.</returns>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IStatistics Statistics(Channels channels);

    /// <summary>
    /// Strips an image of all profiles and comments.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Strip();

    /// <summary>
    /// Channel a texture on image background.
    /// </summary>
    /// <param name="image">The image to use as a texture on the image background.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Texture(IMagickImage image);

    /// <summary>
    /// Threshold image.
    /// </summary>
    /// <param name="percentage">The threshold percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Threshold(Percentage percentage);

    /// <summary>
    /// Threshold image.
    /// </summary>
    /// <param name="percentage">The threshold percentage.</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Threshold(Percentage percentage, Channels channels);

    /// <summary>
    /// Compose an image repeated across and down the image.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Tile(IMagickImage image, CompositeOperator compose);

    /// <summary>
    /// Compose an image repeated across and down the image.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Tile(IMagickImage image, CompositeOperator compose, string? args);

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <returns>A base64 <see cref="string"/>.</returns>
    string ToBase64();

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <returns>A base64 <see cref="string"/>.</returns>
    string ToBase64(MagickFormat format);

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A base64 <see cref="string"/>.</returns>
    string ToBase64(IWriteDefines defines);

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[] ToByteArray();

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    byte[] ToByteArray(IWriteDefines defines);

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    byte[] ToByteArray(MagickFormat format);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. This
    /// requires the image to have a color profile. Nothing will happen if the image has no color profile.
    /// </summary>
    /// <param name="target">The target color profile.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    bool TransformColorSpace(IColorProfile target);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. This
    /// requires the image to have a color profile. Nothing will happen if the image has no color profile.
    /// </summary>
    /// <param name="target">The target color profile.</param>
    /// <param name="mode">The color transformation mode.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    bool TransformColorSpace(IColorProfile target, ColorTransformMode mode);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. The
    /// source profile will only be used if the image does not contain a color profile. Nothing
    /// will happen if the source profile has a different colorspace then that of the image.
    /// </summary>
    /// <param name="source">The source color profile.</param>
    /// <param name="target">The target color profile.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    bool TransformColorSpace(IColorProfile source, IColorProfile target);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. The
    /// source profile will only be used if the image does not contain a color profile. Nothing
    /// will happen if the source profile has a different colorspace then that of the image.
    /// </summary>
    /// <param name="source">The source color profile.</param>
    /// <param name="target">The target color profile.</param>
    /// <param name="mode">The color transformation mode.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    bool TransformColorSpace(IColorProfile source, IColorProfile target, ColorTransformMode mode);

    /// <summary>
    /// Trim edges that are the background color from the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Trim();

    /// <summary>
    /// Trim the specified edges that are the background color from the image.
    /// </summary>
    /// <param name="edges">The edges that need to be trimmed.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Trim(params Gravity[] edges);

    /// <summary>
    /// Trim edges that are the background color from the image.
    /// </summary>
    /// <param name="percentBackground">The percentage of background pixels permitted in the outer rows and columns.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Trim(Percentage percentBackground);

    /// <summary>
    /// Apply a white balancing to an image according to a grayworld assumption in the LAB colorspace.
    /// </summary>
    void WhiteBalance();

    /// <summary>
    /// Apply a white balancing to an image according to a grayworld assumption in the LAB colorspace.
    /// </summary>
    /// <param name="vibrance">The vibrance.</param>
    void WhiteBalance(Percentage vibrance);

    /// <summary>
    /// Forces all pixels above the threshold into white while leaving all pixels at or below
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void WhiteThreshold(Percentage threshold);

    /// <summary>
    /// Forces all pixels above the threshold into white while leaving all pixels at or below
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <param name="channels">The channel(s) to make black.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void WhiteThreshold(Percentage threshold, Channels channels);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(FileInfo file);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(FileInfo file, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(FileInfo file, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(Stream stream);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(Stream stream, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(Stream stream, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(string fileName);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(string fileName, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(string fileName, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, IWriteDefines defines, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, IWriteDefines defines, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, IWriteDefines defines, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, MagickFormat format, CancellationToken cancellationToken);
}
