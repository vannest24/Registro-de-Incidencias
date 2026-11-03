import { useEffect, useState } from "react";

type Forecast = { date: string; temperatureC: number; summary: string | null };

const API_URL = import.meta.env.VITE_API_URL ?? "http://localhost:5000";

function App() {
  const [data, setData] = useState<Forecast[]>([]);
  const [error, setError] = useState("");

  useEffect(() => {
    fetch(`${API_URL}/weatherforecast`)
      .then((r) => r.json())
      .then(setData)
      .catch((e) => setError(String(e)));
  }, []);

  return (
    <div>
      <h1>Prueba de conexión</h1>
      {error && <p style={{ color: "red" }}>Error: {error}</p>}
      <ul>
        {data.map((f) => (
          <li key={f.date}>
            {f.date}: {f.temperatureC}°C, {f.summary}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
