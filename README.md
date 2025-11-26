# Meteoro
Meteoro es un modelo de Machine Learning implementado desde cero en C# y dotNET 9.0,  no se utiliza ninguna librería de NuGet. Actualmente el modelo se entrena con datos de ejemplo en el runtime.

## Explicación
Como la predicción de la temperatura es más o menos una razón de cambio, resulta y acontence que esto es un problema bastante comun cuando se habla de Machine Learning. A esto se le dice Serie Temporal y en resumen se intentan modelar datos historicos para lograr predecir datos futuros.

Esta es una red neuronal de una sola neurona con data muy limitada, así que los resultados no son extremadamente acertados. Como aqui solo se predice la temperatura sin tomar nada más en cuenta, esto "funciona" pero no es ideal, ya que en el mundo real la temperatura se obtiene de varios factores.

## Preguntas Frecuentes
### Si pero porque?
Estaba aburrido.

### Porque no ML.NET?
Eso es hacer trampa.

## Limitaciones y planes
- No se puede conseguir data historica.
- No es muy acertada.
- Se debe de entrenar cada vez que se ejecuta en el runtime.
- Solo se toma en cuenta la temperatura pasada y ni un otro factor.
- Se debe predecir el clima en su totalidad (Soleado, lluvia, vientos, etc.)
