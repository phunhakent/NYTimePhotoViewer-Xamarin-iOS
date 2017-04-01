using System;
using Foundation;
using NYTPhotoViewer;
using UIKit;

namespace Example
{
	public class CustomPhoto : NYTPhoto
	{
		UIImage image;
		UIImage bgImage; 

		public CustomPhoto(string imageUrl, UIImage bgImage = null): base()
		{ 
			this.bgImage = bgImage;
			ImageUrl = imageUrl;
		}

		public string ImageUrl
		{
			get;
			set;
		}

		public override NSAttributedString AttributedCaptionCredit
		{
			get; set;
		}

		public override NSAttributedString AttributedCaptionSummary
		{
			get; set;
		}

		public override NSAttributedString AttributedCaptionTitle
		{
			get; set;
		}

		public override UIImage Image
		{
			get
			{ return this.image; }
			set
			{ this.image = value; }
		}

		public override NSData ImageData
		{
			get; set;
		}

		public override UIImage PlaceholderImage
		{ 
			get
			{ return this.bgImage; }
			set
			{ this.bgImage = value; }
		}
	}
}
