<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="InfoCenter.aspx.cs" Inherits="FYP.InfoCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link href="CSS/InfoCenter.css" rel="stylesheet" type="text/css" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="googleMapSection">
        <h1 class="googleMapHeader">Google Map</h1><br />
        <!--  <p id="demo"></p>  -->
        <table class="topTable">
            <tr>
                <td><input id="input" type="text" style="height:75px;width:99%;float:left"/></td>
                <td>
                    <div class="main">
                        <input id="slider" type="range" min="0" max="10000" value="5000" onclick ="suggestedResult()"/>
                            <div id="selector">
                                <div id="selectBtn"></div>
                                <div id="selectValue"></div>
                            </div>
                        <div id="progressBar"></div>
                    </div>
                </td>
            </tr>
        </table>
    
   
        <table class="table">
            <tr>
                <td><div id="googleMap" class="googleMapCss"></div> </td>
                <td>
                    <!-- Radio button -->
                
                    <div class="searchedResults">
                        <div class="radio-container">
                            <input type="radio" id="recommended" name="option" value="recommended" checked="checked" onclick ="suggestedResult()">
                            <label for="recommended">Recommended</label>

                            <input type="radio" id="nearest" name="option" value="male" onclick ="suggestedResult()">
                            <label for="nearest">Nearest</label>

                            <input type="radio" id="mostRated" name="option" value="mostRated" onclick ="suggestedResult()">
                            <label for="mostRated">Most Rated</label>

                            <input type="radio" id="highestRating" name="option" value="highestRating"  onclick ="suggestedResult()">
                            <label for="highestRating">Highest Rating</label>

                            <input type="radio" id="showAll" name="option" value="showAll" onclick ="suggestedResult()">
                            <label for="showAll">Show All</label>
                        </div>
                                           
                        <h2>Suggested location</h2>

                        <p id="demo"></p>

                                          
                    </div>

                    <div class="autoCompleteResult">
                        <h2>Searched location</h2>
                        <p id="autoComplete"></p>
                        <p>*Make sure location on device and browser is enabled to get accurate location.</p>
                    </div>
                </td>
            </tr>
        </table><br />
    </div>
    
    <div class ="weatherSection">
        <h1 class="weatherHeader">Weather</h1>
        <table class="table">
            <tr>
                <td>
                    <div class="container">
                        <div class="app-title">
                            <p>Weather</p>
                        </div>
                        <div class="notification"> </div>
                        <div class="weather-container">
                            <div class="weather-icon">
                                <img src="icons/unknown.png" alt="">
                            </div>
                            <div class="temperature-value">
                                <p>- °<span>C</span></p>
                            </div>
                            <div class="temperature-description">
                                <p> - </p>
                            </div>
                            <div class="location">
                                <p>-</p>
                                
                            </div>
                        </div>
                    </div>
                </td>
                <td>
                    <div id="weatherInfo"></div>
                    <div style="padding-left:1%"><p>*Enable https connection to get weather Info.<br/><br/>
                    <a href="https://cors-anywhere.herokuapp.com/}" class="navigateButton">Enable Https connection</a></p></div>
                </td>
            </tr>
        </table>
    </div>
 
<!--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCOs73MjrEETY_b9lbU9QCco5DoMll2UOY&sensor=false">  
</script>   -->

 
<script async
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCOs73MjrEETY_b9lbU9QCco5DoMll2UOY&libraries=places">
</script>

  
    <script type="text/javascript">  


        //===============================================================
        //Google Map=====================================================
        //===============================================================
        var slider = document.getElementById('slider');
        var radius = slider.value;
        var selector = document.getElementById('selector');
        var selectValue = document.getElementById('selectValue');
        var progressBar = document.getElementById('progressBar');

        selectValue.innerHTML = slider.value + "m";

        slider.oninput = function () {
            radius = this.value;
            selectValue.innerHTML = this.value + "m";
            selector.style.left = this.value/100 + "%";
            progressBar.style.width = this.value/100 + "%";
            
        }

        //document.getElementById("demo").innerHTML = 5 + 6;
        $(document).ready(function () {
            // Add your function call here
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(success);
            } else {
                alert("Geolocation not supported!");
            }
        });

        let markers = []; //google place results' markers array
        var highestRatingMarker = [];
        var mostRatedMarker = [];
        var nearestMarker = [];
        var recommendedMarker = [];
        let map;
        let service;
        let infowindow;
        let lat, long, LatLng;
        let searchedResults = [];
        const googleMapLink = "https://www.google.com/maps/dir/";
        const image = "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png";

        function success(position) {
            //GOOGLE_MAP--------
            var name = getParameterByName('name');
            lat = position.coords.latitude;
            long = position.coords.longitude;
            LatLng = new google.maps.LatLng(lat, long);
            

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
                    + lat +"<br />Longitude: " + long
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
            document.getElementById('autoComplete').innerHTML = "Search a location using the search bar above the google map.";

            autocomplete = new google.maps.places.Autocomplete(document.getElementById("input"), {
                fields: ['geometry', 'name', 'rating', 'formatted_address'],
                componentRestrictions: { 'country': ['MY'] },
                types:['establishment']
            });

            autocomplete.addListener("place_changed", () => {
                const place = autocomplete.getPlace();
                var searchedLink = googleMapLink + lat + "," + long + "/" + place.geometry.location;
                var searchedMarker = new google.maps.Marker({
                    position: place.geometry.location,
                    title: place.name,
                    icon: image,
                    map: map,
                    animation: google.maps.Animation.DROP,
                });

                placeLink = googleMapLink + lat + "," + long + "/" + place.geometry.location;

                document.getElementById('autoComplete').innerHTML = (`${place.name} is the searched location with a rating of ${place.rating} on google. <br/><br/>
                                                                Location Address : ${place.formatted_address}<br/><br/>
                                                                <a href="${placeLink}" class="navigateButton">Navigate Now</a>`);
                //var d = distance(LatLng, place.geometry.location);
                
                //alert(d);
                

                searchedMarker.setMap(map);
                

                var getDestinationInfoWindow = new google.maps.InfoWindow({

                    content: "<b>Searched Place <br><hr><br>" + place.name + "</b><br>" + place.geometry.location + "<br/><br/><a href=\"" + searchedLink + "\" style=\"font-size:large;text-decoration:none\">Navigate</a><br/>" 
                });

                //click to open info window
                searchedMarker.addListener("click", () => {
                    getDestinationInfoWindow.open(map, searchedMarker);
                });
            });


            //GOOGLE_PLACE SEARCH
            if (name != null) {
                showAutoSearch();
                var request = {
                    location: LatLng,
                    radius: 500,
                    query: name,
                    //fields: ["name", "geometry"],
                    //types: ["point_of_interest", "establishment"]
                    //openNow: true;
                };
                
                service = new google.maps.places.PlacesService(map);

                service.textSearch(request, callback);
            } else {
                hideAutoSearch();
                hideSlider();
                document.getElementById('demo').innerHTML = "No location found...";
            }

           
        }

        //javascript version weather
        getWeather(lat, long);

        function hideSlider() {
            var x = document.getElementsByClassName('main');
            x[0].style.display = 'none';
        }

        function showSlider() {
            var x = document.getElementsByClassName('main');
            x[0].style.display = 'block';
        }

        //radio button and the content
        function hideAutoSearch() {
            var x = document.getElementsByClassName('searchedResults');
            x[0].style.display = 'none';
          
        }

        function showAutoSearch() {
            var x = document.getElementsByClassName('searchedResults');
            x[0].style.display = 'block';

        }

        function callback(results, status) {
            if (status == google.maps.places.PlacesServiceStatus.OK) {
                
                for (var i = 0; i < results.length; i++) {
                    createMarker(results[i], markers);
                    //console.log(results[i]);
                }
                searchedResults = results;//save the result for further use
                suggestedResult(); //create marker and write result
            } 
        }

        function getNearestLocation() {
            hideSlider();
            //nearest location 
            
            var nearestDistance = distance(LatLng, searchedResults[0].geometry.location);
            var newNearestDistance;
            var nearestLocation = searchedResults[0];
            
            //console.log(searchedResults[0].name +  " >> initial distance :" + nearestDistance);

            for (var i = 1; i < searchedResults.length; i++) {

                newNearestDistance = distance(LatLng, searchedResults[i].geometry.location);
                //console.log(searchedResults[i].name + " >>  distance :" + newNearestDistance);

                if (newNearestDistance < nearestDistance ) {
                    nearestLocation = searchedResults[i];
                    nearestDistance = newNearestDistance;
                }

            }

            if (nearestMarker.length == 0) {
                createMarker(nearestLocation, nearestMarker);
            }

            showMarkers(nearestMarker);

            return [nearestLocation , nearestDistance];
        }

        function getHighestRatingLocation() {
            //highest rating
            showSlider();
            var highestRatingLocation;
            var isFirstTime = true;

            for (var i = 0; i < searchedResults.length; i++) {

                var d = distance(LatLng, searchedResults[i].geometry.location);
                if (d <= radius) { //limit the highest rating location

                    if (isFirstTime) {
                        highestRatingLocation = searchedResults[i];
                        isFirstTime = false;
                    }

                    if (highestRatingLocation.rating < searchedResults[i].rating) {
                        highestRatingLocation = searchedResults[i];

                    }
                }
            }

            return highestRatingLocation;
        }



        function getMostRatedLocation() {
            //most rated 
            showSlider();
            var mostRatedLocation;
            var isFirstTime = true;

            for (var i = 0; i < searchedResults.length; i++) {

                var d = distance(LatLng, searchedResults[i].geometry.location);
                if (d <= radius) { //limit the highest rating location

                    if (isFirstTime) {
                        mostRatedLocation = searchedResults[i];
                        isFirstTime = false;
                    }

                    if (mostRatedLocation.user_ratings_total < searchedResults[i].user_ratings_total) {
                        mostRatedLocation = searchedResults[i];

                    }
                }
            }

            return mostRatedLocation;
        }

        //need to have the results
        function suggestedResult() {

            //NEAREST=======================================
            if (document.getElementById('nearest').checked) {

                clearMarkers(highestRatingMarker);
                clearMarkers(markers);
                clearMarkers(mostRatedMarker);
                clearMarkers(recommendedMarker);

                var nearestLocationLink;

                //get nearest location
                var nearestValues = getNearestLocation();

                var nearestLocation = nearestValues[0];
                var nearestDistance = nearestValues[1];

                if (nearestLocation != null) {

                    nearestLocationLink = googleMapLink + lat + "," + long + "/" + nearestLocation.geometry.location;

                    document.getElementById("demo").innerHTML = (`Location : ${nearestLocation.name} is the nearest location around you. <br/><br/>
                                                                  Distance : ${nearestDistance.toFixed(2)} meters <br/><br/>
                                                                  Rating : ${nearestLocation.rating} on google. <br/><br/>
                                                                Location Address : ${nearestLocation.formatted_address}<br/><br/>
                                                                <a href="${nearestLocationLink}" class="navigateButton">Navigate Now</a>`);
                }

                //HIGHEST RATING=======================================
            } else if (document.getElementById('highestRating').checked) {

                clearMarkers(markers);//clear all google place searched markers
                clearMarkers(nearestMarker);
                clearMarkers(mostRatedMarker);
                clearMarkers(recommendedMarker);

                var highestRatingLink = googleMapLink;

                var highestRatingLocation = getHighestRatingLocation();

                if (highestRatingLocation != null) {

                    highestRatingLink = googleMapLink + lat + "," + long + "/" + highestRatingLocation.geometry.location;

                    document.getElementById("demo").innerHTML = (`Location : ${highestRatingLocation.name} is the highest rated location in ${radius}m<br/><br/>
                                                                   Rating : ${highestRatingLocation.rating} on google. <br/><br/>
                                                                Location Address : ${highestRatingLocation.formatted_address}<br/><br/>
                                                                <a href="${highestRatingLink}" class="navigateButton">Navigate Now</a>`);

                    deleteMarkers(highestRatingMarker);
                    createMarker(highestRatingLocation, highestRatingMarker);
                    //console.log(highestRatingLocation.user_ratings_total);

                } else {
                    deleteMarkers(highestRatingMarker);
                    document.getElementById("demo").innerHTML = (`No location found...`);
                }

                showMarkers(highestRatingMarker);

                //MOST RATED=======================================
            } else if (document.getElementById('mostRated').checked) {

                clearMarkers(markers);//clear all google place searched markers
                clearMarkers(nearestMarker);
                clearMarkers(highestRatingMarker);
                clearMarkers(recommendedMarker);

                var mostRatedLink = googleMapLink;

                var mostRatedLocation = getMostRatedLocation();

                if (mostRatedLocation != null) {

                    mostRatedLink = googleMapLink + lat + "," + long + "/" + mostRatedLocation.geometry.location;

                    document.getElementById("demo").innerHTML = (`Location : ${mostRatedLocation.name} is the most rated location in ${radius}m.<br/><br/>
                                                                  Number of rating(s) : ${mostRatedLocation.user_ratings_total} ratings <br/><br/> 
                                                                    Rating : ${mostRatedLocation.rating} on google. <br/><br/>
                                                                Location Address : ${mostRatedLocation.formatted_address}<br/><br/>
                                                                <a href="${mostRatedLink}" class="navigateButton">Navigate Now</a>`);

                    deleteMarkers(mostRatedMarker);
                    createMarker(mostRatedLocation, mostRatedMarker);
                    //console.log(highestRatingLocation.user_ratings_total);

                } else {
                    deleteMarkers(mostRatedMarker);
                    document.getElementById("demo").innerHTML = (`No location found...`);
                }

                showMarkers(mostRatedMarker);

                //RECOMMENDED=======================================
            } else if (document.getElementById('recommended').checked) {
                hideSlider();
                var recommendedLocation; var m_allLocationAverage; var totalRatings = 0;
                var ratingList = []; var C_lowerQuartile;var limitRange = 2500;var count = 0;
                var isFirstTime = true; var highestBayesianRating; var newHighestBayesianRating;
                var recommendedLink;

                //recommended location considering three factors 
                clearMarkers(markers);//clear all google place searched markers
                clearMarkers(nearestMarker);
                clearMarkers(highestRatingMarker);
                clearMarkers(mostRatedMarker);

                /*C_lowerQuartile = Quartile(ratingList, 0.25);

                if (C_lowerQuartile == 0) {
               
                }*/

                //console.log(`m = ${m_allLocationAverage} and c = ${C_lowerQuartile} count= ${count} total rating = ${totalRatings}`);
                do { // do until location found

                    for (var i = 0; i < searchedResults.length; i++) {

                        var d = distance(LatLng, searchedResults[i].geometry.location);

                        if (d < limitRange) {
                            totalRatings += searchedResults[i].rating;
                            ratingList.push(searchedResults[i].user_ratings_total);
                            count++;
                            //console.log(searchedResults[i]);
                        }
                    }

                    m_allLocationAverage = totalRatings / count;
                    C_lowerQuartile = mean(ratingList);
                   
                    for (var i = 0; i < searchedResults.length; i++) {
                        var d = distance(LatLng, searchedResults[i].geometry.location);

                        if (d < limitRange) {
                            if (isFirstTime) {
                                recommendedLocation = searchedResults[i];
                                highestBayesianRating = calculateBayesAverage(searchedResults[i].user_ratings_total
                                    , searchedResults[i].rating, m_allLocationAverage, C_lowerQuartile);
                                console.log("first = " + highestBayesianRating);
                                isFirstTime = false;
                            }

                            newHighestBayesianRating = calculateBayesAverage(searchedResults[i].user_ratings_total
                                , searchedResults[i].rating, m_allLocationAverage, C_lowerQuartile);

                            console.log("new= " + newHighestBayesianRating);
                            console.log(searchedResults[i]);

                            if (newHighestBayesianRating > highestBayesianRating) {
                                highestBayesianRating = newHighestBayesianRating;
                                recommendedLocation = searchedResults[i];
                            }

                        }
                    }
                    limitRange += 1000;
                    console.log("Highest = " + highestBayesianRating);
                }while(recommendedLocation == null || highestBayesianRating < 3.5)

                /*
                for (var i = 0; i < searchedResults.length; i++) {
                if (C_lowerQuartile == 0) {
                    C_lowerQuartile = 2;
                }

                newHighestBayesianRating = calculateBayesAverage(searchedResults[i].user_ratings_total
                    , searchedResults[i].rating, m_allLocationAverage, C_lowerQuartile);
                console.log(i + 1 + "Rating = " + newHighestBayesianRating);
                console.log(searchedResults[i]);
                }*/

                if (recommendedLocation != null) {
                    if (recommendedMarker.length == 0) { createMarker(recommendedLocation, recommendedMarker); }

                    recommendedLink = googleMapLink + lat + "," + long + "/" + recommendedLocation.geometry.location;
                    document.getElementById("demo").innerHTML = (`Location: ${recommendedLocation.name}
                                                                    <br/><br/> Bayesian Rating : ${highestBayesianRating.toFixed(2)}
                                                                    <br/><br/> Location Address : ${recommendedLocation.formatted_address}
                                                                    <br/><br/><a href="${recommendedLink}" class="navigateButton">Navigate Now</a> `);
                } else {
                    document.getElementById("demo").innerHTML = "No location found...";
                }

                showMarkers(recommendedMarker);
                
            } else {

                //show all
                hideSlider();
                //create marker
                clearMarkers(highestRatingMarker);
                clearMarkers(nearestMarker);
                clearMarkers(mostRatedMarker);
                clearMarkers(recommendedMarker);
                showMarkers(markers);
               

                if (searchedResults.length != 0) {
                    document.getElementById('demo').innerHTML = (`<b>Click on the marker to see detailed informations.</b><br/><br/>
                                                              Click the navigation link to proceed to navigate in google map.
                                                               You are able to click the link in the google map to send location
                                                               to your smartphone and proceed to your destination. <br/><br/>
                                                               Have a nice day~~~`);
                }
            }

        }
                                        //user_rating_total      //rating
        function calculateBayesAverage(product_ratings_count, product_ratings_average, m_allLocationAverage, C_lowerQuartile) {
            if (product_ratings_count == 0) {
                return 0;
            } else {
                return (product_ratings_count * product_ratings_average + m_allLocationAverage * C_lowerQuartile)
                    / (product_ratings_count + C_lowerQuartile)
            }            
        }

        function mean(numbers) {
            var total = 0, i;
            for (i = 0; i < numbers.length; i += 1) {
                total += numbers[i];
            }
            return total / numbers.length;
        }

        //sort array
        function Array_Sort_Numbers(inputarray) {
            return inputarray.sort(function (a, b) {
                return a - b;
            });
        }

        //get rating quartile
        function Quartile(data, q) {
            data = Array_Sort_Numbers(data);
            var pos = ((data.length) - 1) * q;
            var base = Math.floor(pos);
            var rest = pos - base;
            if ((data[base + 1] !== undefined)) {
                return data[base] + rest * (data[base + 1] - data[base]);
            } else {
                return data[base];
            }
        }

        // marker functions
        // Sets the map on all markers in the array.
        function setMapOnAll(map , array) {
            for (let i = 0; i < array.length; i++) {
                array[i].setMap(map);
            }
        }

        // Removes the markers from the map, but keeps them in the array.
        function clearMarkers(array) {
            setMapOnAll(null,array);
        }

        // Shows any markers currently in the array.
        function showMarkers(array) {
            setMapOnAll(map,array);
        }

        // Deletes all markers in the array by removing references to them.
        function deleteMarkers(array) {
            clearMarkers(array);
            array.length = 0;
        }

        //Returns Distance between two latlng objects using haversine formula
        function distance(p1, p2) {
            if (!p1 || !p2)
                return 0;
            var R = 6371000; // Radius of the Earth in m
            var dLat = (p2.lat() - p1.lat()) * Math.PI / 180;
            var dLon = (p2.lng() - p1.lng()) * Math.PI / 180;
            var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
                Math.cos(p1.lat() * Math.PI / 180) * Math.cos(p2.lat() * Math.PI / 180) *
                Math.sin(dLon / 2) * Math.sin(dLon / 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var d = R * c;
            return d;
        }

        function createMarker(place, array) {

            if (!place.geometry || !place.geometry.location) {
                return;
            }
            var link = googleMapLink + lat + "," + long + "/" + place.geometry.location;

            const marker = new google.maps.Marker({
                map,
                position: place.geometry.location,
                icon: image,
            });

            var placeInfoWindow = new google.maps.InfoWindow({
                content: "<b>Searched Place <br><hr><br>" + place.name + "</b><br>" + place.geometry.location + "<br><br/><a href=\"" + link + "\" style=\"font-size:large;text-decoration:none\">Navigate</a><br/>" 
            });

            google.maps.event.addListener(marker, "click", () => {
                placeInfoWindow.open(map, marker);
            });

            array.push(marker);
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

        //===============================================================
        //Weather=====================================================
        //===============================================================

        const iconElement = document.querySelector(".weather-icon");
        const tempElement = document.querySelector(".temperature-value p");
        const descElement = document.querySelector(".temperature-description p");
        const locationElement = document.querySelector(".location p");
        const notificationElement = document.querySelector(".notification");

        // App data
        const weather = {};

        weather.temperature = {
            unit: "celsius"
        }

        // APP CONSTS AND VARS
        const KELVIN = 273;
        // API KEY
        const key = "f3f718fb3d54bf852baf842135e157c5";

        // GET WEATHER FROM API PROVIDER
        function getWeather(latitude, longitude) {
            let api = `https://cors-anywhere.herokuapp.com/http://api.openweathermap.org/data/2.5/weather?lat=${latitude}&lon=${longitude}&appid=${key}`;

            fetch(api)
                .then(function (response) {
                    let data = response.json();
                    
                    return data;
                })
                .then(function (data) {
                    console.log(data);
                    weather.temperature.maxTemp = data.main.temp_max - KELVIN;
                    weather.temperature.minTemp = data.main.temp_min - KELVIN;
                    weather.humidity = data.main.humidity;
                    weather.wind = data.wind.speed;
                    weather.temperature.value = Math.floor(data.main.temp - KELVIN);// get celcius
                    weather.description = data.weather[0].description;
                    weather.iconId = data.weather[0].icon;
                    weather.city = data.name;
                    weather.country = data.sys.country;
                })
                .then(function () {
                    displayWeather();
                });
        }

        // DISPLAY WEATHER TO UI
        function displayWeather() {
            document.getElementById('weatherInfo').innerHTML = (`Humudity : ${weather.humidity}% <br/><br/>
                                                                 Maximum Temperature : ${weather.temperature.maxTemp.toFixed(2)}°<span>C</span><br/><br/>
                                                                 Minimum Temperature : ${weather.temperature.minTemp.toFixed(2)}°<span>C</span><br/><br/>
                                                                 Wind Speed : ${weather.wind} meter per second <br/><br/>  
                                                                 `);

            iconElement.innerHTML = `<img src="icons/${weather.iconId}.png"/>`;
            tempElement.innerHTML = `${weather.temperature.value}°<span>C</span>`;
            descElement.innerHTML = weather.description;
            locationElement.innerHTML = `${weather.city}, ${weather.country}`;
        }

        // C to F conversion
        function celsiusToFahrenheit(temperature) {
            return (temperature * 9 / 5) + 32;
        }

        // WHEN THE USER CLICKS ON THE TEMPERATURE ELEMENET
        tempElement.addEventListener("click", function () {
            if (weather.temperature.value === undefined) return;

            if (weather.temperature.unit == "celsius") {
                let fahrenheit = celsiusToFahrenheit(weather.temperature.value);
                let maxFahrenheit = celsiusToFahrenheit(weather.temperature.maxTemp);
                let minFahrenheit = celsiusToFahrenheit(weather.temperature.minTemp);

                fahrenheit = Math.floor(fahrenheit);
                document.getElementById('weatherInfo').innerHTML = (`Humudity : ${weather.humidity}% <br/><br/>
                                                                 Maximum Temperature : ${maxFahrenheit.toFixed(2)}°<span>F</span><br/><br/>
                                                                 Minimum Temperature : ${minFahrenheit.toFixed(2)}°<span>F</span><br/><br/>
                                                                 Wind Speed : ${weather.wind} meter per second <br/><br/>
                                                               
                                                                 `);
                tempElement.innerHTML = `${fahrenheit}°<span>F</span>`;
                weather.temperature.unit = "fahrenheit";
            } else {
                document.getElementById('weatherInfo').innerHTML = (`Humudity : ${weather.humidity}% <br/><br/>
                                                                 Maximum Temperature : ${weather.temperature.maxTemp.toFixed(2)}°<span>C</span><br/><br/>
                                                                 Minimum Temperature : ${weather.temperature.minTemp.toFixed(2)}°<span>C</span><br/><br/>
                                                                 Wind Speed : ${weather.wind} meter per second <br/><br/>   
                                                              
                                                                 `);
                tempElement.innerHTML = `${weather.temperature.value}°<span>C</span>`;
                weather.temperature.unit = "celsius"
            }
        });

    </script>  

<!--<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCOs73MjrEETY_b9lbU9QCco5DoMll2UOY&callback=myMap"></script>-->

</asp:Content>
