import { useEffect, useState } from "react";

interface Reading {
  id: number;
  deviceId: number;
  sensorType: string;
  value: number;
  unit: string;
  timestamp: string;
}

export default function App() {
  const [readings, setReadings] = useState<Reading[]>([]);

  useEffect(() => {
    fetch("/api/readings")
      .then((res) => res.json())
      .then((data) => setReadings(data))
      .catch((err) => console.error("Failed to load data:", err));
  }, []);

  return (
    <div className="p-6">
      {/* Title with Tailwind classes */}
      <h1 className="text-4xl font-bold text-green-600 mb-6">
        🌱 Smart Greenhouse Dashboard
      </h1>

      {/* Data list */}
      <ul className="space-y-2">
        {readings.map((r) => (
          <li
            key={r.id}
            className="p-4 rounded-xl bg-white shadow-md border border-gray-200"
          >
            <div className="text-lg font-semibold text-gray-800">
              Device #{r.deviceId}
            </div>
            <div className="text-gray-600">
              {r.sensorType}:{" "}
              <span className="font-bold">
                {r.value} {r.unit}
              </span>
            </div>
            <div className="text-sm text-gray-500">
              {new Date(r.timestamp).toLocaleString()}
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}
