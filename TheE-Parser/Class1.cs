using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheE_Parser
{
    class Class1
    {
        public class ICanOCR : MonoBehaviour {

	class LockedFastImage
	{
		private Texture2D image;
		private byte[] rgbValues
		
		public LockedFastImage(Texture2D image)
		{
			this.image = new Texture2D(image.width, image.height);
			this.image.LoadImage(image.EncodeToPNG());
		}

		/// <summary>
		/// Returns or sets a pixel of the image. 
		/// </summary>
		/// <param name="x">x parameter of the pixel</param>
		/// <param name="y">y parameter of the pixel</param>
		public Color this[int x, int y]
		{
			get 
			{
				return image.GetPixel(x,y);
			}
			
			set 
			{
				image.SetPixel(x,y, value);
			}
		}
		
		/// <summary>
		/// Width of the image. 
		/// </summary>
		public int Width
		{
			get
			{
				return image.width;
			}
		}
		
		/// <summary>
		/// Height of the image. 
		/// </summary>
		public int Height
		{
			get
			{
				return image.height;
			}
		}
		
		/// <summary>
		/// Returns the modified Bitmap. 
		/// </summary>
		public Texture2D asTexture()
		{
			return image;
		}
	}
	
	class ImageChecker
	{
		
		private LockedFastImage big_image;
		private LockedFastImage small_image; 
		/// <summary>
		/// The time needed for last operation.
		/// </summary>
		///
		public TimeSpan time_needed = new TimeSpan();
		
		/// <summary>
		/// Error return value.
		/// </summary>

		static public Vector2 CHECKFAILED = new Vector2(-1f, -1f);
		
		/// <summary>
		/// Constructor of the ImageChecker
		/// </summary>
		/// <param name="big_image">The image containing the small image.</param>
		/// <param name="small_image">The image located in the big image.</param>
		public ImageChecker(Texture2D big_image, Texture2D small_image)
		{
			this.big_image = new LockedFastImage(big_image);
			this.small_image = new LockedFastImage(small_image);
		}
		
		/// <summary>
		/// Returns the location of the small image in the big image. Returns CHECKFAILED if not found.
		/// </summary>
		/// <param name="x_speedUp">speeding up at x achsis.</param>
		/// <param name="y_speedUp">speeding up at y achsis.</param>
		/// <param name="begin_percent_x">Reduces the search rect. 0 - 100</param>
		/// <param name="end_percent_x">Reduces the search rect. 0 - 100</param>
		/// <param name="begin_percent_x">Reduces the search rect. 0 - 100</param>
		/// <param name="end_percent_y">Reduces the search rect. 0 - 100</param>
		public Point bigContainsSmall(int x_speedUp = 1, int y_speedUp = 1, int begin_percent_x = 0, int end_percent_x = 100, int begin_percent_y = 0, int end_percent_y = 100)
		{
			/*
             * SPEEDUP PARAMETER
             * It might be enough to check each second or third pixel in the small picture.
             * However... In most cases it would be enough to check 4 pixels of the small image for diablo porposes.
             * */
			
			/*
             * BEGIN, END PARAMETER
             * In most cases we know where the image is located, for this we have the begin and end paramenters.
             * */
			
			DateTime begin = DateTime.Now;
			
			if (x_speedUp < 1) x_speedUp = 1;
			if (y_speedUp < 1) y_speedUp = 1;
			if (begin_percent_x < 0 || begin_percent_x > 100) begin_percent_x = 0;
			if (begin_percent_y < 0 || begin_percent_y > 100) begin_percent_y = 0;
			if (end_percent_x   < 0 || end_percent_x   > 100) end_percent_x   = 100;
			if (end_percent_y   < 0 || end_percent_y   > 100) end_percent_y   = 100;
			
			int x_start = (int)((double)big_image.Width *  ((double)begin_percent_x / 100.0));
			int x_end   = (int)((double)big_image.Width *  ((double)end_percent_x   / 100.0));
			int y_start = (int)((double)big_image.Height * ((double)begin_percent_y / 100.0));
			int y_end   = (int)((double)big_image.Height * ((double)end_percent_y   / 100.0));
			
			/*
             * We cant speed up the big picture, because then we have to check pixels in the small picture equal to the speeded up size 
             * for each pixel in the big picture.
             * Would give no speed improvement.
             * */
			
			//+ 1 because first pixel is in picture. - small because image have to be fully in the other image
			for (int x = x_start; x < x_end - small_image.Width + 1; x++)
				for (int y = y_start; y < y_end - small_image.Height + 1; y++)
			{
				//now we check if all pixels matches
				for (int sx = 0; sx < small_image.Width; sx += x_speedUp)
					for (int sy = 0; sy < small_image.Height; sy += y_speedUp)
				{
					if (small_image[sx, sy] != big_image[x + sx, y + sy])   
						goto CheckFailed;
				}
				
				//check ok
				time_needed = DateTime.Now - begin;
				return new Point(x, y);
				
			CheckFailed: ;
			}
			
			time_needed = DateTime.Now - begin;
			return CHECKFAILED;
		}
	}
}

    }
}
