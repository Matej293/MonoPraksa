function createCity(name, country, population) {
    const city = {
        Name: name,
        Country: country,
        Population: population
    };

    return city;
}

const cities = [];
function createCityFromForm() {
    const name = document.getElementById("nameIn").value;
    const country = document.getElementById("countryIn").value;
    const population = parseInt(document.getElementById("populationIn").value, 10);

    const cityExists = cities.find(city => city.Name === name && city.Country === country);

    if (cityExists) {
        alert("This city already exists in the database.");
    }
    else {
        const city = createCity(name, country, population);
        cities.push(city);

        localStorage.setItem('cityObjects', JSON.stringify(cities));

        displayCities();
    }
}


function displayCities() {
    const cityListContainer = document.getElementById("cityObjects");
    cityListContainer.innerHTML = '';

    cities.forEach((city, index) => {
        const cityContainer = document.createElement("div");
        cityContainer.className = "city-container";

        const cityDiv = document.createElement("div");
        cityDiv.innerHTML = `
            <strong>Name:</strong> ${city.Name}<br>
            <strong>Country:</strong> ${city.Country}<br>
            <strong>Population:</strong> ${city.Population}<br>
        `;

        const deleteButton = document.createElement("button");
        deleteButton.innerHTML = "Delete";
        deleteButton.addEventListener("click", () => deleteCity(index));

        cityContainer.appendChild(cityDiv);
        cityContainer.appendChild(deleteButton);

        cityListContainer.appendChild(cityContainer);
    });

    if (cities.length === 0) {
        cityListContainer.style.display = 'none';
    } else {
        cityListContainer.style.display = 'block';
    }
}

function deleteCity(index) {
    cities.splice(index, 1);
    localStorage.setItem('cityObjects', JSON.stringify(cities));
    displayCities();
}
