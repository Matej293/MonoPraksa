import React, { useState } from "react";

export const CityForm = ({ addCity }) => {
  const [cityName, setCityName] = useState("");
  const [country, setCountry] = useState("");
  const [population, setPopulation] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();

    const cityObject = {
      cityName,
      country,
      population: parseInt(population),
    };

    addCity(cityObject);

    setCityName("");
    setCountry("");
    setPopulation("");
  };
  return (
    <div>
      <form onSubmit={handleSubmit} className="CityForm">
        <div>
          <label>
            City Name:
            <input
              type="text"
              value={cityName}
              onChange={(e) => setCityName(e.target.value)}
              className="city-input"
            />
          </label>
        </div>
        <div>
          <label>
            Country:
            <input
              type="text"
              value={country}
              onChange={(e) => setCountry(e.target.value)}
              className="city-input"
            />
          </label>
        </div>
        <div>
          <label>
            Population:
            <input
              type="number"
              value={population}
              onChange={(e) => setPopulation(e.target.value)}
              className="city-input"
            />
          </label>
        </div>
        <button type="submit">Submit</button>
      </form>
    </div>
  );
};

export default CityForm;
