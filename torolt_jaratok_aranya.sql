-- Törölt járatok aránya
SELECT carrier,carrier_name,SUM(arr_flights)/SUM(arr_del15)  FROM flights
  GROUP BY carrier
  ORDER BY carrier;