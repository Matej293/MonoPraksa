import React, { useState } from "react";

export const EditCityForm = ({ city, editCity }) => {
  const [editedCityName, setEditedCityName] = useState(city.cityName);
  const [editedCountry, setEditedCountry] = useState(city.country);
  const [editedPopulation, setEditedPopulation] = useState(city.population);

  const handleUpdate = (e) => {
    e.preventDefault();

    const editedCity = {
      id: city.id,
      cityName: editedCityName,
      country: editedCountry,
      population: parseInt(editedPopulation),
    };

    editCity(editedCity);
  };

  return (
    <div>
      <form onSubmit={handleUpdate} className="CityForm">
        <div>
          <label>
            City Name:
            <input
              type="text"
              value={editedCityName}
              onChange={(e) => setEditedCityName(e.target.value)}
              className="city-input"
            />
          </label>
        </div>
        <div>
          <label>
            Country:
            <input
              type="text"
              value={editedCountry}
              onChange={(e) => setEditedCountry(e.target.value)}
              className="city-input"
            />
          </label>
        </div>
        <div>
          <label>
            Population:
            <input
              type="number"
              value={editedPopulation}
              onChange={(e) => setEditedPopulation(e.target.value)}
              className="city-input"
            />
          </label>
        </div>
        <button type="submit">Update</button>
      </form>
    </div>
  );
};

export default EditCityForm;
