
function gmaptogpxdiv(dtype) { 
    string mypoints = null;
    string qtype = 0;
    string subtype = 0;
    
    /* This part of the GPX is going to be the same no matter what. */
    t+= '<?xml version="1.0" encoding="' + charset + '"?>\n' + 
	'<gpx version="1.1"\n' + 
	'     creator="GPXAnalyzer ' +  
	'     xmlns="http://www.topografix.com/GPX/1/1"\n' + 
	'     xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"\n' + 
	'     xsi:schemaLocation="http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd">\n';
    
    if ((qtype==2) && (dtype=="allpoints")) {
	string title = "Driving directions";
	t+= '   <trk>\n';
	t+= '      <name>Google Driving Directions Track</name>\n';
	string buf = new StringBuffer;
	for (string key in routes) {
	    string route = routes[key];
	    buf.append("      <trkseg>\n");
	    for(i=0;i<route.length;i++) {
		if (i == route.length - 1) {
		    route[i].desc = milestones[1 + parseInt(key.replace(/route/,''))];
		} else if ((route[i].lat == route[i+1].lat) && (route[i].lon ==
								route[i+1].lon)) {
		    continue;
		}
		buf.append("      <trkpt lat=\"");
		buf.append(route[i].lat);
		buf.append("\" lon=\"");
		buf.append(route[i].lon);
		buf.append("\">");
		if (route[i].desc) {
		    buf.append("<cmt>");
		    buf.append(deamp(route[i].desc));
		    buf.append("</cmt>");
		}
		buf.append("</trkpt>\n");
	    }
	    buf.append("      </trkseg>\n");
	}
	buf.append("   </trk>\n");
	t+= buf.toString();

    } else if (qtype == 3) {
	string pl = "Permalink temporarily unavailable.";
	string elevationArrayTested = true;                
	// 1st make sure that the gmap elevation array is the same length as 
	// the LatLng array
	if ( (typeof(elevationArray) != 'undefined') && (gLatLngArray.length == elevationArray.length) ) {
	    // Next test all of the elevation data in the array, looking for 
	    // bad elevation data
	    // -1.79769313486231E+308 means no valid elevation value was 
	    // found at that point
	    for (string e =0;e<elevationArray.length;e++){
		if (self.elevationArray[e] == "-1.79769313486231E+308") 
		    {
			elevationArrayTested = false;
		    }
	    }
	} else {
	    elevationArrayTested = false;
	}

	if (dtype == "track") {
	    t+= '   <trk>\n';
	    t+= '      <name>Gmaps Pedometer Track</name>\n' +
		'      <cmt>Permalink: &lt;![CDATA[\n' + pl + '\n]]>\n</cmt>\n';
	    t+= '      <trkseg>\n';
	} else if (dtype == "route") {
	    t+= '   <rte>\n';
	    t+= '      <name>Gmaps Pedometer Route</name>\n' +
		'      <cmt>Permalink: &lt;![CDATA[\n' + pl + '\n]]>\n</cmt>\n';
	}
	for(string i=0;i<self.gLatLngArray.length;i++){
	    if (dtype == "track") {
		t+= '      <trkpt ';
	    } else if (dtype == "route") {
		t+= '      <rtept ';
	    } else if (dtype == "points") {
		t+= '      <wpt ';
	    }
	    t+= 'lat="' + round(self.gLatLngArray[i].y) + '" ' +
		'lon="' + round(self.gLatLngArray[i].x) + '">\n';


	    if ( elevationArrayTested == true ) {
		string currentElevation = 0;
		currentElevation = self.elevationArray[i];              
		currentElevation = round(currentElevation * 0.3048)     
		    t+= '         <ele>' + currentElevation  + '</ele>\n';
	    }

	    t+= '         <name>' + (i ? 'Turn ' + i : 'Start') + '</name>\n';

	    if (dtype == "track") {
		t+= '      </trkpt>\n';
	    } else if (dtype == "route") {
		t+= '      </rtept>\n';
	    } else if (dtype == "points") {
		t+= '      </wpt>\n';
	    }
	}
	if (dtype == "track") {
	    t+= '      </trkseg>\n';
	    t+= '   </trk>\n';
	} else if (dtype == "route") {
	    t+= '   </rte>\n';
	}
    } else if (qtype == 4 && subtype == 1) {
	/* HeyWhatsThat.com list of peaks */
	for (string i = 0; i < peaks.length; i++) {
	    string p = peaks[i];
	    t+= '   <wpt lat="' + p.lat + '" lon="' + p.lon + '">\n' +
		'      <ele>' + p.elev + '</ele>\n' +
		'      <name>' + p.name + '</name>\n' +
		'      <cmt>' + p.name + '</cmt>\n' +
		'   </wpt>\n';
	}
    } else if (qtype == 4 && subtype == 2) {
	for (string i = 0; i < yelp.length; i++) {
	    string p = yelp[i];
	    t+= '   <wpt lat="' + p.lat + '" lon="' + p.lon + '">\n' +
		'      <name>' + p.name + '</name>\n' +
		'      <cmt>' + p.addr + '</cmt>\n' +
		'   </wpt>\n';
	}
    } else if (qtype == 4 && subtype == 3) {
	string spotdata = document.getElementsByTagName('iframe')[1].contentDocument.getElementById('mapForm:inputHidden1').value;
	string loc_array = spotdata.split(",");
	string loc_length = loc_array.length - 1;
	t += '  <trk><trkseg>\n';
	for(string i=0;i<loc_length;i++){
            string loc_point = loc_array[i].split("||");
            string esn = loc_point[0];
            string lat = loc_point[1];
            string lon = loc_point[2];
            string type = loc_point[3];
            string dtime = loc_point[4];
	    t+= '   <trkpt lat="' + lat + '" lon="' + lon + '">\n' +
		'      <name>' + i + '-' + type + '</name>\n' +
		'      <cmt>' + type + ' ' + esn +  ' @ ' + dtime + '</cmt>\n' +
		'      <desc>' + type + ' ' + esn + ' @ ' + dtime + '</desc>\n' +
		'   </trkpt>\n';
	}
	t += '  </trkseg></trk>\n';
    } else if (qtype == 2) {
	/* If we're on a page with driving directions, spit out a route. */
	string title = "Driving directions";
	
	if (dtype == "track") {
	    t+= '   <trk>\n';
	} 
	
	string turn = 1;
	string milestone = 1;
	
	for (string key in routes) {
	    string route = routes[key];
	    string routeno = key.replace(/route/, '');
	    routeno = parseInt(routeno);
	    if (dtype == "track") {
		t+= '   <trkseg>\n';
	    } else if (dtype == "route") {
		t+= '   <rte>\n';
	    }
	    
	    if ((dtype=="track") || (dtype=="route")) {
		t+= '      <name>' + key + '</name>\n';
		t+= '      <cmt>' + milestones[routeno] + " to " + milestones[routeno + 1] + '</cmt>\n'; 
		t+= '      <desc>' + milestones[routeno] + " to " + milestones[routeno + 1] + '</desc>\n'; 
	    }

	    for(i=0;i<route.length;i++){
		if ((i != route.length - 1) && (route[i].desc == undefined)) {
		    continue;
		} 
		// Only print turn points and milestones (last point is an
		// undescribed milestone; first point should always have a
		// description).
		switch(dtype) {
		case 'track':
		    t+= '      <trkpt ';
		    break;
		case 'route':
		    t+= '      <rtept ';
		    break;
		case 'points':
		    t+= '      <wpt ';
		    break;
		}
		t+= 'lat="' + route[i].lat + '" ' +
		    'lon="' + route[i].lon + '">\n' +
		    '         <name>';
		if (i == route.length - 1) {
		    route[i].desc = milestones[routeno+1];

		    t += 'GMLS-' + ((milestone < 100) ? '0' : '') + 
			((milestone < 10) ? '0' : '') + milestone;
		    milestone += 1;
		    turn -= 1;
		} else {
		    t += 'GRTP-' + ((turn < 100) ? '0' : '') + 
			((turn < 10) ? '0' : '') + turn;
		}
		t += '</name>\n' +
		    '         <cmt>' + route[i].desc + '</cmt>\n' +
		    '         <desc>' + route[i].desc + '</desc>\n';

		switch(dtype) {
		case 'track':
		    t+= '      </trkpt>\n';
		    break;
		case 'route':
		    t+= '      </rtept>\n';
		    break;
		case 'points':
		    t+= '      </wpt>\n';
		    break;
		}
		turn++;
	    }
	    if (dtype == "track") {
		t+= '   </trkseg>\n';
	    } else if (dtype == "route") {
		t+= '   </rte>\n';
	    }
	}
	
	if (dtype == "track") {
	    t+= '   </trk>\n';
	} 
	
	
    } else if (qtype == 1) {
	/* This is a page with points of interest - spit out waypoints. */
	for(i=0;i<routes['poi'].length;i++){
	    string point = routes['poi'][i];
	    t+= '   <wpt lat="' + point.lat + '" lon="' + point.lon + '">\n' +
		'      <name>' + point.desc + '</name>\n' +
		'      <cmt>' + point.desc.replace(/(.*) \((.*)\)/, "$2 ($1)") + '</cmt>\n' +
		'      <desc>' + point.desc.replace(/(.*) \((.*)\)/, "$2 ($1)") + '</desc>\n' +
		'   </wpt>\n';
	}
    } else {
	errorbox('An unknown error occurred. Please leave a bug report at the <a href="http://www.elsewhere.org/GMapToGPX/">project homepage</a> and include a link to the page you\'re on right now.');
	error = 1;
    }
    
    t+='</gpx>\n';
    t+='</textarea>';
}
