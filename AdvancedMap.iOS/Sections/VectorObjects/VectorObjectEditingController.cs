﻿
using System;
using Carto.Core;
using Carto.DataSources;
using Carto.Layers;
using Shared;
using Shared.iOS;

namespace AdvancedMap.iOS
{
	public class VectorObjectEditingController : MapBaseController
	{
		public override string Name { get { return "Vector object editing"; } }

		public override string Description { get { return "Shows usage of an editable vector layer"; } }

		LocalVectorDataSource source;

		EditableVectorLayer editLayer;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			AddOnlineBaseLayer(CartoBaseMapStyle.CartoBasemapStyleVoyager);

			// Initialize source and Edit layer, add it to the map
			source = new LocalVectorDataSource(MapView.Options.BaseProjection);

			editLayer = new EditableVectorLayer(source);
			MapView.Layers.Add(editLayer);

			// Convenience methods to add elements to the map, cf. LocalVectorDataSourceExtensions
			source.AddPoint(new MapPos(-7000000, 7000000));

			source.AddLine(new MapPosVector { 
				new MapPos(-9000000, -9000000), new MapPos(-5000000, -500000)
			});

			source.AddPolygon(new MapPosVector {
				new MapPos(-4000000, -4000000), new MapPos(4000000, -4000000), new MapPos(0, 7000000)
			});

			// Add a vector element even listener to select elements (on element click)
			editLayer.VectorElementEventListener = new VectorElementSelectEventListener(editLayer);

			// Add a map even listener to deselect element (on map click)
			MapView.MapEventListener = new VectorElementDeselectEventListener(editLayer);

			// Add the vector element edit even listener
			editLayer.VectorEditEventListener = new BasicEditEventListener(source);
		}

		public override void ViewDidUnload()
		{
			base.ViewDidUnload();

			editLayer.VectorElementEventListener = null;

			MapView.MapEventListener = null;

			editLayer.VectorEditEventListener = null;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			Alert("Click on object to modify or move it");
		}
	}
}

