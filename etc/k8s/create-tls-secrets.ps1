mkcert "exchange.dev" "*.exchange.dev" 
kubectl create namespace exchange
kubectl create secret tls -n exchange exchange-tls --cert=./exchange.dev+1.pem  --key=./exchange.dev+1-key.pem