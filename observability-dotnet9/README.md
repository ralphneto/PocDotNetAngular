# Observability .NET 9 Sample

Conteúdo:
- Backend/: Projeto ASP.NET Core (.NET 9) com OpenTelemetry + Serilog (Loki)
- docker-compose.yml: Grafana stack (Prometheus, Loki, Tempo, Grafana, OTEL Collector)
- prometheus.yml, otel-collector-config.yaml, loki-local-config.yaml, tempo-local-config.yaml

## Como rodar (local)
1. Tenha Docker e Docker Compose instalados.
2. Na raiz do repositório, rode:
   ```
   docker-compose up --build
   ```
3. Acesse:
   - Grafana: http://localhost:3000 (admin/admin)
   - Prometheus: http://localhost:9090
   - Loki: http://localhost:3100
   - Tempo: http://localhost:3200
   - API Backend: http://localhost:5000
4. Prometheus target do dotnet está configurado para `host.docker.internal:9464`.
   Certifique-se de habilitar o exporter Prometheus no app (o projeto já inclui AddPrometheusExporter()).

## Observações
- Ajuste endpoints conforme sua topologia (kubernetes, cloud).
- Para produção, proteja Grafana, habilite TLS e use backends de armazenamento persistente para Loki/Tempo.
