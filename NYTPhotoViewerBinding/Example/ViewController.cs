using System;
using System.Collections.Generic;
using Foundation;
using NYTPhotoViewer;
using UIKit;

namespace Example
{
	public partial class ViewController : UIViewController, INYTPhotosViewControllerDelegate
	{
		List<CustomPhoto> photos;

		/// <summary>
		/// The photos list.
		/// </summary>
		List<string> captionSummary;

		/// <summary>
		/// The index of the caption summary.
		/// </summary>
		int captionSummaryIndex;
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			///caption summary list
			captionSummary = new List<string>();
			captionSummary.Add("photo with custom everything");
			captionSummary.Add("photo with long caption. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum maximus laoreet vehicula. Maecenas elit quam, pellentesque at tempor vel, tempus non sem. ");

			//default value 0
			captionSummaryIndex = 0;

			//UIButton
			btn = new UIButton()
			{
				Frame = new CoreGraphics.CGRect(100, 100, 200, 200)
			};
			View.Add(btn);
			btn.Center = View.Center;
			btn.SetBackgroundImage(UIImage.FromFile("cartoon1.jpg"), UIControlState.Normal);
			btn.TouchUpInside += btnTouchUpInside;
		}
		UIButton btn;
		void btnTouchUpInside(object sender, EventArgs e)
		{
			UIButton button = (UIButton)sender;

			//NYTPhotos list
			photos = new List<CustomPhoto>();

			 


			photos.Add(new CustomPhoto("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJNHNhHYq9OJ02RYLVQH686JITNs4gossJ9cGHLDW8TeKD4Su3", UIImage.FromFile("cartoon1.jpg")));
			photos.Add(new CustomPhoto("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR5yBSI9sff9O3yrhsMzXJUtrA-PtdgmYyVkNZNxtbGr582olk6", UIImage.FromFile("cartoon1.jpg")));

			var a1 = photos.ToArray();

			NYTPhotosViewController controller = new NYTPhotosViewController(a1, a1[0]);
			controller.WeakDelegate = this; 
			this.PresentViewController(controller, true, () => 
			{ 
				SDWebImage.SDWebImageManager.SharedManager.Download(new NSUrl(a1[0].ImageUrl), SDWebImage.SDWebImageOptions.RefreshCached, null, (image, error, cacheType, finished, imageUrl) =>
				{
					a1[0].Image = image;
					controller.UpdateImageForPhoto(a1[0]);
				});
			});
		}

		[Export("photosViewController:didNavigateToPhoto:atIndex:")]
		void DidNavigateToPhotoAtIndex(NYTPhotosViewController photosViewController, NYTPhoto photo, nuint photoIndex)
		{
			var a = photo as CustomPhoto;
			SDWebImage.SDWebImageManager.SharedManager.Download(new NSUrl(a.ImageUrl), SDWebImage.SDWebImageOptions.RefreshCached, null, (image, error, cacheType, finished, imageUrl) => 
			{
				a.Image = image;
				photosViewController.UpdateImageForPhoto(a);
			});
		}

		[Export("photosViewController:maximumZoomScaleForPhoto:")]
		public nfloat MaximumZoomScaleForPhoto(NYTPhotosViewController photosViewController, NYTPhoto photo)
		{
			return 5.0f;
		}

		[Export("photosViewController:referenceViewForPhoto:")]
		public UIView ReferenceViewForPhoto(NYTPhotosViewController photosViewController, NYTPhoto photo)
		{
			//return base.ReferenceViewForPhoto(photosViewController, photo);
			return btn;
		} 

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
