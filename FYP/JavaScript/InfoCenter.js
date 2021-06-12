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