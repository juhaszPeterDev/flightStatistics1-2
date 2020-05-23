-- Leglátogatottabb repterek légitársaságonként
SELECT carrier, carrier_name, airport,airport_name, MAX(ossz) FROM
(
  SELECT carrier,carrier_name,airport,airport_name, SUM(arr_flights) as ossz
    FROM flights3
    GROUP BY carrier, airport
    ORDER BY carrier, ossz DESC
) x 
GROUP BY carrier
;