<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="InfoCenter.aspx.cs" Inherits="FYP.InfoCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link href="CSS/InfoCenter.css" rel="stylesheet" type="text/css" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Google Map</h2><br />
    
    <input id="input" type="text" style="height:75px;width:49.5%"/>
    <table class="table">
        <tr>
            <td><div id="googleMap" class="googleMapCss"></div> </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
        
 

<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCOs73MjrEETY_b9lbU9QCco5DoMll2UOY&sensor=false">  
</script>  
  
<script async
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCOs73MjrEETY_b9lbU9QCco5DoMll2UOY&libraries=places&callback=success">
</script>

  
    <script type="text/javascript">  
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(success);
        } else {
            alert("Geolocation not supported!");
        }

        let map;
        let service;
        let infowindow;
        let lat, long;
        const googleMapLink = "https://www.google.com/maps/dir/";
        const image = "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png";

        function success(position) {

            //GOOGLE_MAP--------
            var name = getParameterByName('name');
            lat = position.coords.latitude;
            long = position.coords.longitude;
            var LatLng = new google.maps.LatLng(lat, long);
            

            //https://www.google.com/maps/dir/2.8000601238492435,%2B101.49562181470077/hospital%20banting
            //GOOGLE_MAP PROPERTIES
            map = new google.maps.Map(document.getElementById("googleMap"),{
                center: LatLng,
                zoom: 14,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            //INITIAL PROPERTIES
            var initMarker = new google.maps.Marker({
                position: LatLng,
                animation: google.maps.Animation.DROP,
                title: "<div style = 'height:60px;width:200px'><b>Your location:</b><br />Latitude: "
                    + lat + +"<br />Longitude: " + long
            });
            initMarker.setMap(map);
            var getInfoWindow = new google.maps.InfoWindow({
                content: "<b>Your Current Location</b><br/> Latitude:" +
                    lat + "<br /> Longitude:" + long + "<br/><a href=\"" 
            });

            //getInfoWindow.open(map, initMarker);
            //click to open info window
            initMarker.addListener("click", () => {
                getInfoWindow.open(map, initMarker);
            });


            //AUTO_COMPLETE------------------------------
            autocomplete = new google.maps.places.Autocomplete(document.getElementById("input"), {
                fields: ['geometry', 'name'],
                componentRestrictions: { 'country': ['MY'] },
                types:['establishment']
            });

            autocomplete.addListener("place_changed", () => {
                const place = autocomplete.getPlace();
                var searchedLink = googleMapLink + lat + "," + long + "/" + place.name;
                var searchedMarker = new google.maps.Marker({
                    position: place.geometry.location,
                    title: place.name,
                    icon: image,
                    map: map,
                    animation: google.maps.Animation.DROP,
                });

                searchedMarker.setMap(map);
                

                var getDestinationInfoWindow = new google.maps.InfoWindow({

                    content: "<b>Searched Place <br>" + place.name + "</b><br>" + place.geometry.location + "<br/><a href=\"" + searchedLink + "\" style=\"font-size:large\">Navigate</a><br/>" 
                });

                //click to open info window
                searchedMarker.addListener("click", () => {
                    getDestinationInfoWindow.open(map, searchedMarker);
                });
            });


            //GOOGLE_PLACE SEARCH
            if (name != null) {
                var request = {
                    location: LatLng,
                    radius: '2000',
                    query: name,
                    fields: ["name", "geometry"],
                    types: ['establishment'],
                };

                service = new google.maps.places.PlacesService(map);

                service.textSearch(request, callback);
            }
            
        }


        function callback(results, status) {
            if (status == google.maps.places.PlacesServiceStatus.OK) {
                for (var i = 0; i < results.length; i++) {
                    createMarker(results[i]);
                }
            } 
        }

        function createMarker(place) {


            if (!place.geometry || !place.geometry.location) {
                return;
            }
            var link = googleMapLink;

            const marker = new google.maps.Marker({
                map,
                position: place.geometry.location,
                icon: image,
            });

            link += lat + "," + long + "/" + place.name;

            var placeInfoWindow = new google.maps.InfoWindow({
                content: "<b>Searched Place <br>" + place.name + "</b><br>" + place.geometry.location + "<br/><a href=\"" + link + "\" style=\"font-size:large\">Navigate</a><br/>" 
            });

            google.maps.event.addListener(marker, "click", () => {
                placeInfoWindow.open(map, marker);
            });
        }

        //RETRIEVE QUERY STRING FUNCTION
        function getParameterByName(name, url = window.location.href) {
            name = name.replace(/[\[\]]/g, '\\$&');
            var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, ' '));
        }

    </script>  


<!--<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCOs73MjrEETY_b9lbU9QCco5DoMll2UOY&callback=myMap"></script>-->

</asp:Content>
